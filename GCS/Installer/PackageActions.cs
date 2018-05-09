using System;
using System.Linq;
using System.Xml;
using umbraco.interfaces;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using W3S_GCS.Models.Dtos;

namespace W3S_GCS.Installer {
    public class PackageActions : IPackageAction {

        static DatabaseContext _dbCtx {
            get {
                return ApplicationContext.Current.DatabaseContext;
            }
        }

        static UmbracoDatabase _umDb {
            get {
                return ApplicationContext.Current.DatabaseContext.Database;
            }
        }

        static DatabaseSchemaHelper _dbH {
            get {
                return new DatabaseSchemaHelper(_dbCtx.Database, ApplicationContext.Current.ProfilingLogger.Logger, _dbCtx.SqlSyntax);
            }
        }

        static readonly DatabaseProviders DatabaseProvider = ApplicationContext.Current.DatabaseContext.DatabaseProvider;

        public string Alias() {
            return "w3s-gcs";
        }

        public bool Execute(string packageName, XmlNode xmlData) {
            try {
                LogHelper.Info(System.Reflection.MethodBase.GetCurrentMethod().GetType(), "Executing package actions");
                return InitDatabase();
            } catch (Exception ex) {
                LogHelper.Error(System.Reflection.MethodBase.GetCurrentMethod().GetType(), "INSTALL Package Error", ex);
                return false;
            }
        }

        public static bool InitDatabase() {
            try {
                //if (GetGCSDataBaseExists()) {
                //    throw new Exception("Table already exists.");
                if (_dbH.TableExist("gcs_searchsettings") || _dbH.TableExist("gcs_searchinstance") && _dbH.TableExist("gcs_searchentry")) {
                    throw new Exception("Table already exists.");
                } else {
                    _dbH.CreateTable<SearchEntry>(false);
                    _dbH.CreateTable<SearchSettings>(false);

                    var settings = _umDb.Insert(new SearchSettings() {
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

                    _dbH.CreateTable<SearchInstance>(false);

                    int adminId = _umDb.Query<int>(@"SELECT Id from umbracoUserGroup WHERE userGroupAlias = 'admin'").FirstOrDefault();
                    _dbCtx.Database.Execute(@"insert umbracoUserGroup2App values(@0, @1)", adminId, "GCS");

                    _umDb.Insert(new SearchInstance() {
                        SettingsId = Int32.Parse(settings.ToString()),
                        DateCreated = DateTime.Now.ToString(),
                        Name = "GCS " + settings.ToString()
                    });

                    //_umDb.Query<bool>(GetInitializationQuery());
                    //_umDb.Query<bool>(GetSeederQuery(), new {
                    //    dateNow = DateTime.Now.ToString()
                    //});
                }
                return true;
            } catch (Exception ex) {
                LogHelper.Error(System.Reflection.MethodBase.GetCurrentMethod().GetType(), "Db check error", ex);
                return false;
            }
        }

        public static bool GetGCSDataBaseExists() {
            string query = "SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'searchsettings'";
            return _umDb.Query<int>(query).FirstOrDefault() == 1;
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

        public bool Undo(string packageName, XmlNode xmlData) {
            return true;
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