//namespace W3S_GCS.Migrations
//{
//    using System;
//    using System.Data.Entity.Migrations;

//    public partial class Initial : DbMigration
//    {
//        public override void Up()
//        {
//            CreateTable(
//                "dbo.SearchEntries",
//                c => new
//                    {
//                        Id = c.Int(nullable: false, identity: true),
//                        Query = c.String(maxLength: 4000),
//                        Date = c.DateTime(nullable: false),
//                        TotalCount = c.Int(nullable: false),
//                        Timing = c.Double(nullable: false),
//                        TopResultURL = c.String(maxLength: 4000),
//                        ClickURL = c.String(maxLength: 4000),
//                        ClickTitle = c.String(maxLength: 4000),
//                        HTMLSnippet = c.String(maxLength: 4000),
//                        CorrectedQuery = c.String(maxLength: 4000),
//                    })
//                .PrimaryKey(t => t.Id);

//            CreateTable(
//                "dbo.SearchInstances",
//                c => new
//                    {
//                        Id = c.Int(nullable: false, identity: true),
//                        Name = c.String(maxLength: 4000),
//                        DateCreated = c.String(maxLength: 4000),
//                        SettingsId = c.Int(nullable: false),
//                    })
//                .PrimaryKey(t => t.Id)
//                .ForeignKey("dbo.SearchSettings", t => t.SettingsId, cascadeDelete: true)
//                .Index(t => t.SettingsId);

//            CreateTable(
//                "dbo.SearchSettings",
//                c => new
//                    {
//                        Id = c.Int(nullable: false, identity: true),
//                        BaseURL = c.String(maxLength: 4000),
//                        CXKey = c.String(maxLength: 4000),
//                        APIKey = c.String(maxLength: 4000),
//                        ItemsPerPage = c.Int(nullable: false),
//                        RedirectAlias = c.String(maxLength: 4000),
//                        LoadMoreSetUp = c.String(maxLength: 4000),
//                        MaxPaginationPages = c.Int(nullable: false),
//                        ShowQuery = c.Boolean(nullable: false),
//                        ShowTiming = c.Boolean(nullable: false),
//                        ShowTotalCount = c.Boolean(nullable: false),
//                        ShowSpelling = c.Boolean(nullable: false),
//                        KeepQuery = c.Boolean(nullable: false),
//                        ShowFilterFileType = c.Boolean(nullable: false),
//                        ExcludeTerms = c.String(maxLength: 4000),
//                        ExcludeNodeIds = c.String(maxLength: 4000),
//                        DateRestrict = c.DateTime(),
//                        ShowThumbnail = c.Boolean(nullable: false),
//                        ThumbnailFallbackGUID = c.String(maxLength: 4000),
//                        LoadIconGUID = c.String(maxLength: 4000),
//                        LastUpdated = c.DateTime(),
//                        DateCreated = c.DateTime(),
//                        DevelopmentURL = c.String(maxLength: 4000),
//                    })
//                .PrimaryKey(t => t.Id);

//        }

//        public override void Down()
//        {
//            DropForeignKey("dbo.SearchInstances", "SettingsId", "dbo.SearchSettings");
//            DropIndex("dbo.SearchInstances", new[] { "SettingsId" });
//            DropTable("dbo.SearchSettings");
//            DropTable("dbo.SearchInstances");
//            DropTable("dbo.SearchEntries");
//        }
//    }
//}
