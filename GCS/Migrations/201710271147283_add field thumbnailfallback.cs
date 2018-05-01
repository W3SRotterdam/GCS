namespace GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class addfieldthumbnailfallback : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchSettings", "ThumbnailFallbackGUID", c => c.String());
        }

        public override void Down() {
            DropColumn("dbo.SearchSettings", "ThumbnailFallbackGUID");
        }
    }
}
