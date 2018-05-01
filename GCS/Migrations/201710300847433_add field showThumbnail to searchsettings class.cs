namespace W3S_GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class addfieldshowThumbnailtosearchsettingsclass : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchSettings", "ShowThumbnail", c => c.Boolean(nullable: false));
        }

        public override void Down() {
            DropColumn("dbo.SearchSettings", "ShowThumbnail");
        }
    }
}
