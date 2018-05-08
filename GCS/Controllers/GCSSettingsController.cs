using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using W3S_GCS.Installer;
using W3S_GCS.Models.Dtos;
using W3S_GCS.Repositories;
using W3S_GCS.Services;

namespace W3S_GCS.App_Plugins.GCS.Controllers {
    public class GCSSettingsController : SurfaceController {

        private SettingsRepository SettingsRepository;
        private NodeService NodeService;

        private IDomainService DomainService { get; set; }
        private List<IDomain> Domains { get; set; }

        public GCSSettingsController() {
            SettingsRepository = new SettingsRepository();
            NodeService = new NodeService();
        }

        [HttpPost]
        public JsonResult Get(SearchSettings model) {

            PackageActions.InitDatabase();

            IPublishedContent currentNode = null;
            SearchSettings SearchSettings = SettingsRepository.Get();

            if (DomainService == null) {
                DomainService = Services.DomainService;
            }

            if (Domains == null) {
                Domains = DomainService.GetAll(true).ToList();
            }

            var currentDomain = NodeService.GetCurrentDomain(Domains, model.CurrentURL);

            if (currentDomain != null) {
                currentNode = UmbracoContext.ContentCache.GetById(currentDomain.RootContentId.Value);
                SearchSettings.RedirectNodeURL = NodeService.GetRedirectNodeURL(currentNode, SearchSettings.RedirectAlias);
            } else {
                IPublishedContent searchPage = UmbracoContext.ContentCache.GetAtRoot().FirstOrDefault().DescendantOrSelf(SearchSettings.RedirectAlias);

                if (searchPage != null) {
                    SearchSettings.RedirectNodeURL = UmbracoContext.ContentCache.GetAtRoot().FirstOrDefault().DescendantOrSelf(SearchSettings.RedirectAlias).Url;
                }
            }

            JsonResult json = new JsonResult() {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new {
                    id = SearchSettings.Id,
                    baseUrl = SearchSettings.BaseURL,
                    cxKey = SearchSettings.CXKey,
                    apiKey = SearchSettings.APIKey,
                    itemsPerPage = SearchSettings.ItemsPerPage,
                    redirectPath = !String.IsNullOrEmpty(SearchSettings.RedirectNodeURL) ? SearchSettings.RedirectNodeURL : "",
                    excludeUrls = !String.IsNullOrEmpty(SearchSettings.ExcludeNodeIds) ? String.Join(",", NodeService.GetPathsByUdi(SearchSettings.ExcludeNodeIds)) : "",
                    loadMoreSetUp = SearchSettings.LoadMoreSetUp,
                    maxPaginationPages = SearchSettings.MaxPaginationPages,
                    showQuery = SearchSettings.ShowQuery,
                    showTotalCount = SearchSettings.ShowTotalCount,
                    showTiming = SearchSettings.ShowTiming,
                    showSpelling = SearchSettings.ShowSpelling,
                    excludeTerms = SearchSettings.ExcludeTerms,
                    excludeNodeIds = SearchSettings.ExcludeNodeIds,
                    showFilterFileType = SearchSettings.ShowFilterFileType,
                    showThumbnail = SearchSettings.ShowThumbnail,
                    thumbnailFallback = !String.IsNullOrEmpty(SearchSettings.ThumbnailFallbackGUID) ? NodeService.GetMediaPathByUdi(SearchSettings.ThumbnailFallbackGUID) : "",
                    preloaderIcon = !String.IsNullOrEmpty(SearchSettings.LoadIconGUID) ? NodeService.GetMediaPathByUdi(SearchSettings.LoadIconGUID) : "",
                    developmentURL = SearchSettings.DevelopmentURL,
                    currentNodeId = currentNode.Id,
                    keepquery = SearchSettings.KeepQuery
                }
            };

            return json;
        }

        [HttpPost]
        public JsonResult Set(String model) {
            Boolean success = false;

            try {
                SettingsRepository.Set(JsonConvert.DeserializeObject<SearchSettings>(model));
                success = true;
            } catch {
                success = false;
            }

            JsonResult json = new JsonResult() {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new {
                    success = success,
                }
            };

            return json;
        }
    }
}