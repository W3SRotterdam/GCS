using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using W3S_GCS.Models;
using W3S_GCS.Models.API;
using W3S_GCS.Models.Dtos;
using W3S_GCS.Models.Filters;
using W3S_GCS.Models.Search;
using W3S_GCS.Repositories;
using W3S_GCS.Services;

namespace W3S_GCS.Controllers {
    public class GCSearchController : SurfaceController {
        private SettingsRepository SettingsRepository;
        private QueriesRepository QueriesRepository;
        private PaginationService PaginationService;
        private NodeService NodeService;
        private List<String> APIFields = new List<String>() { "searchInformation", "spelling", "items(title,link,htmlSnippet,formattedUrl)" };
        private UmbracoHelper uh = new UmbracoHelper(UmbracoContext.Current);

        public GCSearchController() {
            QueriesRepository = new QueriesRepository();
            SettingsRepository = new SettingsRepository();
            PaginationService = new PaginationService();
            NodeService = new NodeService();
        }

        [HttpPost]
        public JsonResult Get(SearchRequest model) {
            SearchSettings settings = SettingsRepository.Get();
            SearchResponse SearchResponse = new SearchResponse();
            RootObject obj = new RootObject();
            bool retry = false;
            String FormattedQuery = model.Query;
            String DevelopmentURL = settings.DevelopmentURL;
            JsonResult json = new JsonResult();

            if (ValidateSettings(settings)) {
                //https://stackoverflow.com/questions/30611114/google-custom-search-engine-result-counts-changing-as-results-are-paged
                do {
                    retry = false;
                    IPublishedContent currentNode = UmbracoContext.ContentCache.GetById(model.CurrentNodeID);
                    IPublishedContent rootNode = uh.TypedContentAtRoot().FirstOrDefault(t => t.GetCulture().TwoLetterISOLanguageName == NodeService.GetCurrentCulture(currentNode).TwoLetterISOLanguageName);

                    if (rootNode == null) {
                        rootNode = currentNode.AncestorsOrSelf(2).FirstOrDefault();
                    }

                    if (!String.IsNullOrEmpty(model.Section)) {
                        IPublishedContent node = rootNode.Descendant(model.Section);
                        if (node != null) {
                            FormattedQuery = String.Format("{0} site:{1}", model.Query, !String.IsNullOrEmpty(DevelopmentURL) ? Regex.Replace(node.UrlAbsolute(), @"http.*:\/\/" + HttpContext.Request.Url.DnsSafeHost, DevelopmentURL) : node.UrlAbsolute());
                        }
                    } else {
                        FormattedQuery = String.Format("{0} site:{1}", model.Query, !String.IsNullOrEmpty(DevelopmentURL) ? DevelopmentURL : rootNode.UrlAbsolute());
                    }

                    if (!String.IsNullOrEmpty(model.FileType)) {
                        FormattedQuery = String.Format("{0} filetype:{1}", FormattedQuery, model.FileType);
                    }

                    string URL = string.Format("{0}?filter=1&key={1}&cx={2}&q={3}&start={4}&num={5}&prettyPrint=false", settings.BaseURL, settings.APIKey, settings.CXKey, FormattedQuery, model.StartIndex, settings.ItemsPerPage);

                    if (settings.DateRestrict != null && settings.DateRestrict.Value != null && String.IsNullOrEmpty(model.FileType)) {
                        URL += String.Format("&dateRestrict=d{0}", Math.Abs((DateTime.Now - settings.DateRestrict.Value).Days));
                    }

                    SearchResponse = SearchRequest(URL);

                    obj = JsonConvert.DeserializeObject<RootObject>(SearchResponse.Response);

                    if (obj != null) {
                        retry = int.Parse(obj.searchInformation.totalResults) == 0 && model.StartIndex > 0 ? !retry : retry;
                        model.StartIndex = retry ? model.StartIndex - settings.ItemsPerPage : model.StartIndex;
                    }
                } while
                    (int.Parse(obj.searchInformation.totalResults) == 0 && model.StartIndex > 1 && retry);

                if (settings != null && obj != null && obj.items != null && !String.IsNullOrEmpty(settings.ExcludeNodeIds)) {
                    List<String> urls = NodeService.GetAbsoluteURLByUdi(settings.ExcludeNodeIds);

                    if (urls != null) {
                        foreach (var url in urls) {
                            String _url = url;

                            if (!String.IsNullOrEmpty(DevelopmentURL)) {
                                _url = Regex.Replace(_url, @"http.*:\/\/" + HttpContext.Request.Url.DnsSafeHost, DevelopmentURL);
                            }

                            Item item = obj.items.FirstOrDefault(i => i.link == _url);
                            if (item != null) {
                                obj.items.Remove(item);
                            }
                        }
                    }
                }

                String TopResultURL = obj.items != null && obj.items.Count > 0 && obj.items.First() != null ? obj.items.First().formattedUrl : "";

                if (obj.spelling != null && !String.IsNullOrEmpty(obj.spelling.correctedQuery)) {
                    obj.spelling.correctedQuery = obj.spelling.correctedQuery.Replace(String.Format("site:{0}", DevelopmentURL), "");
                }

                SearchEntry SearchEntry = QueriesRepository.Create(new SearchEntry() {
                    Query = model.Query,
                    Date = DateTime.Now,
                    TotalCount = int.Parse(obj.searchInformation.totalResults),
                    Timing = double.Parse(obj.searchInformation.formattedSearchTime),
                    TopResultURL = TopResultURL,
                    CorrectedQuery = obj.spelling != null ? obj.spelling.correctedQuery : ""
                });

                json = new JsonResult() {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new {
                        success = SearchResponse.Success,
                        list = RenderViewService.GetRazorViewAsString(obj, "~/App_Plugins/W3S_GCS/Views/Partials/SearchResults.cshtml"),
                        spelling = settings.ShowSpelling && obj.spelling != null && !String.IsNullOrEmpty(obj.spelling.correctedQuery) ? RenderViewService.GetRazorViewAsString(new SpellingModel() { CorrectedQuery = obj.spelling.correctedQuery, SearchURL = settings.RedirectNodeURL }, "~/App_Plugins/W3S_GCS/Views/Partials/SearchSpelling.cshtml") : "",
                        totalCount = obj.searchInformation.totalResults,
                        timing = obj.searchInformation.formattedSearchTime,
                        totalPages = Math.Ceiling((double)int.Parse(obj.searchInformation.totalResults) / int.Parse(settings.ItemsPerPage.ToString())),
                        pagination = settings.LoadMoreSetUp == "pagination" ? RenderViewService.GetRazorViewAsString(PaginationService.GetPaginationModel(Request, obj, settings.ItemsPerPage, model.StartIndex, model.Query, model.FileType, model.Section, settings.MaxPaginationPages), "~/App_Plugins/W3S_GCS/Views/Partials/SearchPagination.cshtml") : "",
                        filetypefilter = settings.ShowFilterFileType ? RenderViewService.GetRazorViewAsString(new FileTypeFilter(), "~/App_Plugins/W3S_GCS/Views/Partials/SearchFileTypeFilterSelect.cshtml") : "",
                        queryId = SearchEntry.Id
                    }
                };
            }

            return json;
        }

        public bool ValidateSettings(SearchSettings settings) {
            bool valid = true;
            if (String.IsNullOrEmpty(settings.CXKey)) {
                Logger.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "GCS cx key is missing", null);
                valid = !valid;
            }

            if (String.IsNullOrEmpty(settings.APIKey)) {
                Logger.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "API key is missing", null);
                valid = !valid;
            }

            if (String.IsNullOrEmpty(settings.BaseURL)) {
                Logger.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "GCS base URL is missing", null);
                valid = !valid;
            }

            if (String.IsNullOrEmpty(settings.RedirectAlias)) {
                Logger.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "GCS redirect alias is missing", null);
                valid = !valid;
            }
            return valid;
        }

        public JsonResult ReadMe() {
            return new JsonResult() {
                Data = new {
                    markdown = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/app_plugins/w3s_gcs/README.md"))
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        private string GetFilterSetupAction(string type) {
            string action = "";
            switch (type) {
                case "lists":
                    action = "Select";
                    break;
                case "buttons":
                    action = "Buttons";
                    break;
            }
            return action;
        }

        private SearchResponse SearchRequest(string URL) {
            Boolean success = false;
            String result = "";
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK) {
                    Stream resStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(resStream);
                    result = reader.ReadToEnd();
                    reader.Close();
                    success = true;
                }
            } catch (WebException ex) {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                Stream resStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                ErrorResult errorResult = JsonConvert.DeserializeObject<ErrorResult>(reader.ReadToEnd());

                success = false;
            }

            return new SearchResponse() {
                Response = result,
                StatusCode = statusCode,
                Success = success
            };
        }

        [HttpGet]
        public JsonResult Update() {
            Database.SetInitializer<DBEntities>(new MigrateDatabaseToLatestVersion<DBEntities, W3S_GCS.Migrations.Configuration>());
            using (DBEntities db = new DBEntities()) {
                db.Database.Initialize(true);
            }
            return new JsonResult() {
                Data = "MigrationUpdate",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}