using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using Umbraco.Web.Models.ContentEditing;
using Umbraco.Web.Mvc;
using W3S_GCS.Models.Dtos;
using W3S_GCS.Repositories;
using W3S_GCS.Services;

namespace W3S_GCS.Controllers {
    public class GCSPropertiesController : SurfaceController {

        private InstancesRepository InstancesRepository;
        private SettingsRepository SettingsRepository;
        private SettingsPropertiesService SettingsPropertiesService;
        private SearchSettings settings = null;

        public GCSPropertiesController() {
            SettingsRepository = new SettingsRepository();
            InstancesRepository = new InstancesRepository();
            SettingsPropertiesService = new SettingsPropertiesService();
            settings = SettingsRepository.Get();

            if (settings == null) {
                settings = SettingsRepository.Create();
                InstancesRepository.CreateWithSettingsId(settings.Id);
            }
        }

        public String Get(String alias = "") {
            String data = "";
            switch (alias) {
                case "settings":
                    data = GetSettingsProperties();
                    break;
                case "development":
                    data = GetDevelopmentProperties();
                    break;
                case "statistics":
                    data = GetStatisticsProperties();
                    break;
                case "readme":
                    data = GetReadMeProperties();
                    break;
                default:
                    break;
            }
            return data;
        }

        private String GetSettingsProperties() {
            List<ContentPropertyDisplay> authProperties = new List<ContentPropertyDisplay> {
                new ContentPropertyDisplay {
                    Alias = "baseUrl",
                    Description = "Enter the Google Custom Search API base URL. For more information visit <a href='https://developers.google.com/custom-search/json-api/v1/overview'>Google Custom Search JSON/Atom API.</a>",
                    HideLabel = false,
                    Label = "BaseURL",
                    Validation = new PropertyTypeValidation {Mandatory = true, Pattern = null},
                    View = "textbox",
                    Value = settings.BaseURL
                },
                new ContentPropertyDisplay {
                    Alias = "cxKey",
                    Description = "The custom search engine ID to use for this request. Go to <a href='https://cse.google.com/all' target='_blank'>Google CS console</a> to get the token.",
                    HideLabel = false,
                    Label = "CX Key",
                    Validation = new PropertyTypeValidation {Mandatory = true, Pattern = null},
                    Value = settings.CXKey,
                    View = "textbox"
                },
                new ContentPropertyDisplay {
                    Alias = "apiKey",
                    Description = "JSON/Atom Custom Search API requires the use of an API key. Go to <a href='https://console.developers.google.com/apis/credentials' target='_blank'>Google API console</a>  to create an API key or to retrieve one.",
                    HideLabel = false,
                    Label = "API Key",
                    Validation = new PropertyTypeValidation {Mandatory = true, Pattern = null},
                    Value = settings.APIKey,
                    View = "textbox"
                },
                new ContentPropertyDisplay {
                    Alias = "redirectAlias",
                    Description = "Enter the document type alias of the search results page.",
                    HideLabel = false,
                    Label = "Redirect alias",
                    Validation = new PropertyTypeValidation {Mandatory = true, Pattern = null},
                    Value = settings.RedirectAlias,
                    View = "textbox"
                },
                new ContentPropertyDisplay {
                    Alias = "developmentURL",
                    Description = "When working on a environment other than the production environment enter the absolute (including scheme) indexed domain name.",
                    HideLabel = false,
                    Label = "Development URL",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.DevelopmentURL,
                    View = "textbox"
                },
            };

            List<ContentPropertyDisplay> basicProperties = new List<ContentPropertyDisplay> {
                new ContentPropertyDisplay {
                    Alias = "itemsPerPage",
                    Description = "The number of search results displayed per page.",
                    HideLabel = false,
                    Config = new Dictionary<String, Object>() {
                        { "minNumber", "1" },
                        { "maxNumber", "99" }
                    },
                    Label = "Items per page",
                    Validation = new PropertyTypeValidation {Mandatory = true, Pattern = null},
                    Value = settings.ItemsPerPage,
                    View = "integer"
                },
                new ContentPropertyDisplay {
                    Alias = "loadMoreSetUp",
                    Description = "Configure the way in which you want your users to load more search results.",
                    HideLabel = false,
                    Config = new Dictionary<String, Object>() {
                        { "items", new { Button = "Button", Pagination = "Pagination", Infinite = "Infinite scroll" } },
                        { "multiple", "false" }
                    },
                    Label = "Load more set-up",
                    Validation = new PropertyTypeValidation {Mandatory = true, Pattern = null},
                    Value = settings.LoadMoreSetUp,
                    View = "Dropdown"
                },
                new ContentPropertyDisplay {
                    Alias = "maxPaginationPages",
                    Description = "Configure the maximum amount of total pages (before and after the current active page) will be shown.",
                    HideLabel = false,
                    Label = "Maximum pagination pages",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.MaxPaginationPages,
                    View = "integer"
                },
                new ContentPropertyDisplay {
                    Alias = "showQuery",
                    Description = "Show the search term.",
                    HideLabel = false,
                    Label = "Show search term",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.ShowQuery? "1" : "0",
                    View = "boolean"
                },
                new ContentPropertyDisplay {
                    Alias = "showTiming",
                    Description = "Shows how long the search query took.",
                    HideLabel = false,
                    Label = "Show timing",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.ShowTiming? "1" : "0",
                    View = "boolean"
                },
                new ContentPropertyDisplay {
                    Alias = "showTotalCount",
                    Description = "Shows the total amount of search results.",
                    HideLabel = false,
                    Label = "Show total count",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.ShowTotalCount? "1" : "0",
                    View = "boolean"
                },
                new ContentPropertyDisplay {
                    Alias = "showSpelling",
                    Description = "Show a possible corrected query.",
                    HideLabel = false,
                    Label = "Show spelling",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.ShowSpelling? "1" : "0",
                    View = "boolean"
                },
                new ContentPropertyDisplay {
                    Alias = "keepQuery",
                    Description = "Keep the query in the input field after the search reqeusted is submitted and results are returned.",
                    HideLabel = false,
                    Label = "Keep query",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.KeepQuery? "1" : "0",
                    View = "boolean"
                }
            };

            List<ContentPropertyDisplay> filterProperties = new List<ContentPropertyDisplay> {
                new ContentPropertyDisplay {
                    Alias = "dateRestrict",
                    Description = "Restricts results based on a given date. Only results will be shown which are created on or later than the specified date.",
                    HideLabel = false,
                    Label = "Date restriction",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.DateRestrict,
                    View = "datePicker"
                },
                new ContentPropertyDisplay {
                    Alias = "showFilterFileType",
                    Description = "Shows a filter to search through specific file types such as PDF or .docx",
                    HideLabel = false,
                    Label = "Show file type filter",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.ShowFilterFileType ? "1" : "0",
                    View = "boolean"
                },
                new ContentPropertyDisplay {
                    Alias = "filterSetupFileType",
                    Description = "Configure the way in which you want to show the file type filter",
                    HideLabel = false,
                    Config = new Dictionary<String, Object>() {
                        { "items", new { Buttons = "Buttons", Lists = "Lists" } },
                        { "multiple", "false" }
                    },
                    Label = "Filters file type set-up",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.FilterSetupFileType,
                    View = "Dropdown"
                },
                new ContentPropertyDisplay {
                        Alias = "excludeNodeIds",
                        Description = "Select the nodes to be excluded from the search results",
                        HideLabel = false,
                        Config = new Dictionary<String, Object>() {
                            { "multiPicker", "1" },
                            { "showOpenButton", "0" },
                            { "showEditButton", "0" },
                            { "showPathOnHover", "0" },
                            { "startNode",  new Dictionary<String, Object>() { { "query", "" }, { "type", "content" }, { "id", "-1" } } },
                            { "startNodeId", "-1" },
                            { "idType", "udi" }
                           },
                        Editor = "Umbraco.ContentPicker2",
                        Label = "Exclude nodes",
                        Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                        Value = settings.ExcludeNodeIds,
                        View = "contentpicker"
                },
            };

            List<ContentPropertyDisplay> advancedFilterProperties = new List<ContentPropertyDisplay> {
                new ContentPropertyDisplay {
                    Alias = "documentTypeFilter",
                    Description = "Creates a filter based on the document types that are available",
                    HideLabel = false,
                    Label = "Document types",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    View = "/app_plugins/W3S.CheckboxPlusInput/Views/CheckboxInput.html",
                    Config = new Dictionary<String, object>() {
                        { "items", SettingsPropertiesService.GetDocTypeFilterConfig(settings.DocumentTypeFilter) }
                    },
                },
                new ContentPropertyDisplay {
                    Alias = "filterSetupDocType",
                    Description = "Configure the way in which you want to show the file type filter",
                    HideLabel = false,
                    Config = new Dictionary<String, Object>() {
                        { "items", new { Buttons = "Buttons", Lists = "Lists" } },
                        { "multiple", "false" }
                    },
                    Label = "Filters file type set-up",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.FilterSetupDocType,
                    View = "Dropdown"
                },
            };

            List<ContentPropertyDisplay> stylingFilterProperties = new List<ContentPropertyDisplay> {
                new ContentPropertyDisplay {
                    Alias = "showThumbnail",
                    Description = "Shows the thumbnail image that is stored in GCS metadata.",
                    HideLabel = false,
                    Label = "Show thumbnail",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.ShowThumbnail ? "1" : "0",
                    View = "boolean"
                },
                new ContentPropertyDisplay {
                    Label = "Thumbnail fallback",
                    Description = "Image to show when no suitable thumbnail image is found is GCS metadata.",
                    View = "mediapicker",
                    Config = new Dictionary<String, Object>() {
                        { "startNodeId", -1 },
                        { "idType", "udi" },
                        { "multiPicker", "" },
                        { "startNodeIsVirtual", false },
                        { "onlyImages", "" },
                        { "disableFolderSelect", "" }
                    },
                    HideLabel = false,
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.ThumbnailFallbackGUID,
                    Alias = "thumbnailFallbackGUID",
                    Editor = "Umbraco.MediaPicker2",
                },
                new ContentPropertyDisplay {
                    Alias = "loadIconGUID",
                    Description = "Preloader icon.",
                    Config = new Dictionary<String, Object>() {
                        { "startNodeId", -1 },
                        { "idType", "udi" }
                    },
                    Editor = "Umbraco.MediaPicker2",
                    HideLabel = false,
                    Label = "Preloader icon",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.LoadIconGUID,
                    View = "mediapicker"
                }
            };

            List<ContentPropertyDisplay> genericProperties = new List<ContentPropertyDisplay> {
                new ContentPropertyDisplay {
                    Alias = "lastUpdated",
                    Description = "",
                    HideLabel = false,
                    Label = "Last updated",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.LastUpdated != null ? settings.LastUpdated.Value.ToString("yyyy-MM-dd") : "",
                    View = "readonlyvalue"
                },
                new ContentPropertyDisplay {
                    Alias = "dateCreated",
                    Description = "",
                    HideLabel = false,
                    Label = "Date Created",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.DateCreated != null ? settings.DateCreated.Value.ToString("yyyy-MM-dd") : "",
                    View = "readonlyvalue"
                },
                 new ContentPropertyDisplay {
                    Alias = "id",
                    Description = "",
                    HideLabel = false,
                    Label = "Settings ID",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.Id.ToString(),
                    View = "readonlyvalue"
                },
            };

            List<Tab<ContentPropertyDisplay>> tabs = new List<Tab<ContentPropertyDisplay>> {
                new Tab<ContentPropertyDisplay> {
                    Id = 0,
                    Label = "Auth set-up (admin only)",
                    Properties = authProperties
                },
                new Tab<ContentPropertyDisplay> {
                    Id = 1,
                    Label = "Basic set-up",
                    Properties = basicProperties
                },
                new Tab<ContentPropertyDisplay>() {
                    Id = 2,
                    Label = "Basic filters",
                    Properties = filterProperties
                },
                new Tab<ContentPropertyDisplay>() {
                    Id = 3,
                    Label = "Advanced filters",
                    Properties = advancedFilterProperties
                },
                new Tab<ContentPropertyDisplay>() {
                    Id = 4,
                    Label = "Styling",
                    Properties = stylingFilterProperties
                },
                new Tab<ContentPropertyDisplay>() {
                    Id = 5,
                    Label = "Generic",
                    Properties = genericProperties
                },
            };

            ContentItemDisplay contentItemDisplay = new ContentItemDisplay();
            //contentItemDisplay.Id = 1;
            contentItemDisplay.Name = "Settings (admin only)";
            contentItemDisplay.Tabs = tabs;

            var JsonSettings = new JsonSerializerSettings();
            JsonSettings.ContractResolver = new LowercaseContractResolver();
            var json = JsonConvert.SerializeObject(contentItemDisplay, Formatting.Indented, JsonSettings);

            return json;
        }

        private String GetDevelopmentProperties() {
            List<ContentPropertyDisplay> advancedPropeties = new List<ContentPropertyDisplay> {
                 new ContentPropertyDisplay {
                    Alias = "testing",
                    Description = "",
                    HideLabel = false,
                    Label = "Testing",
                    Validation = new PropertyTypeValidation {Mandatory = false, Pattern = null},
                    Value = settings.Id,
                    View = "readonlyvalue"
                },
            };

            List<Tab<ContentPropertyDisplay>> tabs = new List<Tab<ContentPropertyDisplay>> {
                new Tab<ContentPropertyDisplay> {
                    Id = 1,
                    Label = "Query test",
                    Properties = advancedPropeties
                },
            };

            ContentItemDisplay contentItemDisplay = new ContentItemDisplay();
            //contentItemDisplay.Id = 1;
            contentItemDisplay.Name = "Development";
            contentItemDisplay.Tabs = tabs;

            var JsonSettings = new JsonSerializerSettings();
            JsonSettings.ContractResolver = new LowercaseContractResolver();
            var json = JsonConvert.SerializeObject(contentItemDisplay, Formatting.Indented, JsonSettings);

            return json;
        }

        private String GetStatisticsProperties() {
            List<Tab<ContentPropertyDisplay>> tabs = new List<Tab<ContentPropertyDisplay>>{
                new Tab<ContentPropertyDisplay> {
                    Id = 0,
                    Label = "Top Results",
                    Properties = new List<ContentPropertyDisplay>(),
                    Alias = "stats",
                }
             };

            ContentItemDisplay contentItemDisplay = new ContentItemDisplay();
            //contentItemDisplay.Id = 1;
            contentItemDisplay.Name = "Statistics (admin only)";
            contentItemDisplay.Tabs = tabs;

            var JsonSettings = new JsonSerializerSettings();
            JsonSettings.ContractResolver = new LowercaseContractResolver();
            var json = JsonConvert.SerializeObject(contentItemDisplay);

            return json;
        }

        private String GetReadMeProperties() {
            List<Tab<ContentPropertyDisplay>> tabs = new List<Tab<ContentPropertyDisplay>>{
                new Tab<ContentPropertyDisplay> {
                    Id = 0,
                    Label = "ReadMe",
                    Properties = new List<ContentPropertyDisplay>(),
                    Alias = "readme",
                }
             };

            ContentItemDisplay contentItemDisplay = new ContentItemDisplay();
            //contentItemDisplay.Id = 1;
            contentItemDisplay.Name = "ReadMe";
            contentItemDisplay.Tabs = tabs;

            var JsonSettings = new JsonSerializerSettings();
            JsonSettings.ContractResolver = new LowercaseContractResolver();
            var json = JsonConvert.SerializeObject(contentItemDisplay);

            return json;
        }
    }

    public class LowercaseContractResolver : DefaultContractResolver {
        private List<String> ignoreList = new List<String>() { "multiPicker", "showEditButton", "showOpenButton", "showPathOnHover", "entityType", "minNumber", "maxNumber", "startNode", "hideLabel", "sortOrder", "startNodeId", "idType" };
        protected override string ResolvePropertyName(string propertyName) {
            return !ignoreList.Contains(propertyName) ? propertyName.ToLower() : propertyName;
        }
    }
}