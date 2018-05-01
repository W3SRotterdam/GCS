namespace W3S_GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class initial : DbMigration {
        public override void Up() {
            CreateTable(
                "dbo.SearchEntries",
                c => new {
                    Id = c.Int(nullable: false, identity: true),
                    Query = c.String(),
                    Date = c.DateTime(nullable: false),
                    TotalCount = c.Int(nullable: false),
                    Timing = c.Double(nullable: false),
                    TopResultURL = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SearchInstances",
                c => new {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    DateCreated = c.String(),
                    SettingsId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SearchSettings", t => t.SettingsId, cascadeDelete: true)
                .Index(t => t.SettingsId);

            CreateTable(
                "dbo.SearchSettings",
                c => new {
                    Id = c.Int(nullable: false, identity: true),
                    BaseURL = c.String(),
                    CXKey = c.String(),
                    APIKey = c.String(),
                    ItemsPerPage = c.Int(nullable: false),
                    RedirectNodeId = c.Int(nullable: false),
                    LoadMoreSetUp = c.String(),
                    ShowQuery = c.Boolean(nullable: false),
                    ShowTiming = c.Boolean(nullable: false),
                    ShowTotalCount = c.Boolean(nullable: false),
                    ShowSpelling = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down() {
            DropForeignKey("dbo.SearchInstances", "SettingsId", "dbo.SearchSettings");
            DropIndex("dbo.SearchInstances", new[] { "SettingsId" });
            DropTable("dbo.SearchSettings");
            DropTable("dbo.SearchInstances");
            DropTable("dbo.SearchEntries");
        }
    }
}
