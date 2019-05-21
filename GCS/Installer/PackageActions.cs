using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Umbraco.Core.PackageActions;
using Umbraco.Core.Scoping;
using W3S_GCS.Models.Dtos;

namespace W3S_GCS.Installer
{
    public class PackageActions : IPackageAction {
        private static IScopeProvider _scopeProvider;

        public PackageActions(IScopeProvider scopeProvider) {
            _scopeProvider = scopeProvider;
        }

         public bool Execute(string packageName, XElement xmlData) {
             try {
                //LogHelper.Info(System.Reflection.MethodBase.GetCurrentMethod().GetType(), "GCS Executing package actions");
                return InitDatabase();
            } catch (Exception ex) {
                //LogHelper.Error(System.Reflection.MethodBase.GetCurrentMethod().GetType(), "GCS INSTALL Package Error", ex);
                return false;
            }
        }

        public string Alias() {
            return "w3s-gcs";
        }

        public bool Undo(string packageName, XElement xmlData) {
            throw new NotImplementedException();
        }

        public static bool InitDatabase() {
            using (var scope = _scopeProvider.CreateScope()) {
                if (scope.Database.Query<object>(@"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'umbracoUserGroup2App'").Count() > 0) {
                    int adminId = scope.Database.Query<int>(@"SELECT Id from umbracoUserGroup WHERE userGroupAlias = 'admin'").FirstOrDefault();

                    if (adminId > 0) {
                        if (scope.Database.Query<object>(@"SELECT * FROM umbracoUserGroup2App WHERE userGroupId = @0 AND app = @1", adminId, "GCS").Count() < 1) {
                            scope.Database.Execute(@"insert umbracoUserGroup2App values(@0, @1)", adminId, "GCS");
                        }
                    }
                } else if (scope.Database.Query<object>(@"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'umbracoUser2app'").Count() > 0) {
                    int adminId = scope.Database.Query<int>(@"SELECT Id from umbracoUserType WHERE userTypeAlias = 'admin'").FirstOrDefault();
                    List<int> userIds = scope.Database.Query<int>(@"SELECT Id from umbracoUser WHERE userType = @0", adminId).ToList();

                    if (userIds.Count > 0) {
                        foreach (var id in userIds) {
                            if (scope.Database.Query<object>(@"SELECT * FROM umbracoUser2app WHERE [user] = @0 AND app = @1", id, "GCS").Count() < 1) {
                                scope.Database.Execute(@"insert umbracoUser2app values(@0, @1)", id, "GCS");
                            }
                        }
                    }
                }

                if (!scope.Database.Exists<SearchSettings>("Id") && !scope.Database.Exists<SearchInstance>("Id") && !scope.Database.Exists<SearchEntry>("Id")) {
                    try {
                        scope.Database.Query<object>(GetInitializationQuery());

                        //_dbH.CreateTable<SearchEntry>(false);
                        //_dbH.CreateTable<SearchSettings>(false);

                        var settings = scope.Database.Insert(new SearchSettings() {
                            APIKey = "",
                            CurrentURL = "",
                            CXKey = "",
                            DateCreated = DateTime.Now,
                            DevelopmentURL = "",
                            ExcludeNodeIds = "",
                            LastUpdated = DateTime.Now,
                            DateRestrict = new DateTime(1970, 1, 1, 12, 0, 0, 0),
                            LoadIconGUID = "",
                            RedirectNodeURL = "",
                            ShowFilterFileType = false,
                            ThumbnailFallbackGUID = "",
                            BaseURL = "https://www.googleapis.com/customsearch/v1",
                            RedirectAlias = "search",
                            ItemsPerPage = 10,
                            LoadMoreSetUp = "button",
                            MaxPaginationPages = 6,
                            ShowQuery = true,
                            ShowTiming = true,
                            ShowTotalCount = true,
                            ShowSpelling = true,
                            KeepQuery = true,
                            ShowThumbnail = true,

                        });

                        //_dbH.CreateTable<SearchInstance>(false);

                        scope.Database.Insert(new SearchInstance() {
                            SettingsId = Int32.Parse(settings.ToString()),
                            DateCreated = DateTime.Now.ToString(),
                            Name = "GCS " + settings.ToString()
                        });

                        //_umDb.Query<bool>(GetInitializationQuery());
                        //_umDb.Query<bool>(GetSeederQuery(), new {
                        //    dateNow = DateTime.Now.ToString()
                        //});
                    } catch (Exception ex) {
                        //LogHelper.Error(System.Reflection.MethodBase.GetCurrentMethod().GetType(), "GCS db Error", ex);
                    }
                }
            }

            return true;
        }

        public static bool GetGCSDataBaseExists() {
            using (var scope = _scopeProvider.CreateScope()) {
                string query = "SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'searchsettings'";
                return scope.Database.Query<int>(query).FirstOrDefault() == 1;
            }
        }

        //private static string GetFolderName() {
        //    const string basicFolderName = "W3S_GCS.SQL.";
        //    var folderName = basicFolderName;
        //    if (DatabaseProvider == DatabaseProviders.SqlServerCE) {
        //        folderName += "SqlServerCompact.";
        //    } else {
        //        folderName += "MicrosoftSqlServer.";
        //    }
        //    return folderName;
        //}

        public XmlNode SampleXml() {
            const string sample = "<Action runat=\"install\" undo=\"true\" alias=\"PackageActions\"></Action>";
            return ParseStringToXmlNode(sample);
        }

        private static XmlNode ParseStringToXmlNode(string value) {
            var xmlDocument = new XmlDocument();
            var xmlNode = AddTextNode(xmlDocument, "error", "");

            try {
                xmlDocument.LoadXml(value);
                return xmlDocument.SelectSingleNode(".");
            } catch {
                return xmlNode;
            }
        }

        private static XmlNode AddTextNode(XmlDocument xmlDocument, string name, string value) {
            var node = xmlDocument.CreateNode(XmlNodeType.Element, name, "");
            node.AppendChild(xmlDocument.CreateTextNode(value));
            return node;
        }

        private static Sql GetInitializationQuery() {
            return new Sql(@"CREATE TABLE dbo.SearchEntries (
	                     [Id] int NOT NULL IDENTITY (1,1)
	                    ,[Query] nvarchar(4000) NULL
	                    ,[Date] datetime NOT NULL DEFAULT (getdate())
	                    ,[TotalCount] int NOT NULL
	                    ,[Timing] float
	                    ,[TopResultURL] nvarchar(4000) NULL
	                    ,[ClickURL] nvarchar(4000) NULL
	                    ,[ClickTitle] nvarchar(4000) NULL
	                    ,[HTMLSnippet] nvarchar(4000) NULL
	                    ,[CorrectedQuery] nvarchar(4000) NULL
                    )

                    CREATE TABLE dbo.SearchInstances (
	                     [Id] int NOT NULL IDENTITY (1,1)
	                    ,[Name] nvarchar(4000) NULL
	                    ,[DateCreated] nvarchar(4000) NULL
	                    ,[SettingsId] int NOT NULL
	                    ,CONSTRAINT [FK_dbo.SearchSettings] FOREIGN KEY ([SettingsId]) REFERENCES dbo.SearchSettings(Id)
                    )
    
                    CREATE TABLE dbo.SearchSettings (
	                    [Id] int NOT NULL IDENTITY (1,1)
		                ,[BaseURL] nvarchar(4000) NULL
		                ,[CXKey] nvarchar(4000) NULL
		                ,[APIKey] nvarchar(4000) NULL
		                ,[ItemsPerPage] int NOT NULL
		                ,[RedirectAlias] nvarchar(4000) NULL
		                ,[LoadMoreSetUp] nvarchar(4000) NULL
		                ,[MaxPaginationPages] int NOT NULL
		                ,[ShowQuery] bit NOT NULL DEFAULT ((0))
		                ,[ShowTiming] bit NOT NULL DEFAULT ((0))
		                ,[ShowTotalCount] bit NOT NULL DEFAULT ((0))
		                ,[ShowSpelling] bit NOT NULL DEFAULT ((0))
		                ,[KeepQuery] bit NOT NULL DEFAULT ((0))
		                ,[ShowFilterFileType] bit NOT NULL DEFAULT ((0))
		                ,[ExcludeNodeIds] nvarchar(4000) NULL
		                ,[DateRestrict] datetime NOT NULL DEFAULT (getdate())
		                ,[ShowThumbnail] bit NOT NULL DEFAULT ((0))
		                ,[ThumbnailFallbackGUID] nvarchar(4000) NULL
		                ,[LoadIconGUID] nvarchar(4000) NULL
		                ,[LastUpdated] datetime NOT NULL DEFAULT (getdate())
		                ,[DateCreated] datetime NOT NULL DEFAULT (getdate())
		                ,[DevelopmentURL] nvarchar(4000) NULL
	                )
                )");
        }

        private static string GetSeederQuery() {
            var dateNow = DateTime.Now.ToString();

            return @"
                INSERT INTO dbo.SearchSettings (BaseURL, CXKey, APIKey, ItemsPerPage, LoadMoreSetUp, ShowQuery, ShowTiming, ShowTotalCount, ShowSpelling, KeepQuery, ShowThumbnail)
                VALUES(
                    BaseURL = 'https://www.googleapis.com/customsearch/v1', 
                    RedirectAlias = 'search', 
                    ItemsPerPage = 10, 
                    LoadMoreSetUp = 'button',
                    MaxPaginationPages = 6,
                    ShowQuery = 1
                    ShowTiming =  1
                    ShowTotalCount = 1
                    ShowSpelling = 1
                    KeepQuery = 1
                    ShowThumbnail = 1
                )

                INSERT INTO dbo.SearchInstances (Name, DateCreated, SettingsId)
                VALUES (
                    'GCS 1', @dateNow, 1
                )

                INSERT INTO dbo.umbracoUserGroup2App (userGroupId, app)
                VALUES (
                    (SELECT Id from dbo.umbracoUserGroup WHERE userGroupAlias = 'admin'), 
	                'GCS'
                )
            ";
        }
    }
}