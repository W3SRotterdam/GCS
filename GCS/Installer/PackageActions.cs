using System;
using System.Xml;
using umbraco.BusinessLogic;
using umbraco.DataLayer;
using umbraco.interfaces;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;

namespace W3S_GCS.Installer {
    public class PackageActions : IPackageAction {
        static ISqlHelper _sqlHelper { get { return Application.SqlHelper; } }
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

        public bool InitDatabase() {
            try {
                if (GetGCSDataBaseExists()) {
                    throw new Exception("Table already exists.");
                } else {
                    _sqlHelper.ExecuteNonQuery(GetQuery());
                }
                return true;
            } catch {
                return false;
            }
        }

        public bool GetGCSDataBaseExists() {
            string query = "SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName";
            return _sqlHelper.ExecuteScalar<int>(query, _sqlHelper.CreateParameter("tableName", "dbo.[dbo.SearchInstances]")) == 1;
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

        private static string GetQuery() {
            return @"CREATE TABLE [dbo.SearchEntries] (
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

                    CREATE TABLE [dbo.SearchInstances] (
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
		                ,[ExcludeTerms] nvarchar(4000) NULL
		                ,[ExcludeNodeIds] nvarchar(4000) NULL
		                ,[DateRestrict] datetime NOT NULL DEFAULT (getdate())
		                ,[ShowThumbnail] bit NOT NULL DEFAULT ((0))
		                ,[ThumbnailFallbackGUID] nvarchar(4000) NULL
		                ,[LoadIconGUID] nvarchar(4000) NULL
		                ,[LastUpdated] datetime NOT NULL DEFAULT (getdate())
		                ,[DateCreated] datetime NOT NULL DEFAULT (getdate())
		                ,[DevelopmentURL] nvarchar(4000) NULL
	                )
                )";
        }
    }
}