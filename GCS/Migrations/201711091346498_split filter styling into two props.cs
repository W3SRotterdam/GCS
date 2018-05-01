namespace W3S_GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class splitfilterstylingintotwoprops : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchSettings", "FilterSetupFileType", c => c.String());
            AddColumn("dbo.SearchSettings", "FilterSetupDocType", c => c.String());
            DropColumn("dbo.SearchSettings", "FilterSetup");
        }

        public override void Down() {
            AddColumn("dbo.SearchSettings", "FilterSetup", c => c.String());
            DropColumn("dbo.SearchSettings", "FilterSetupDocType");
            DropColumn("dbo.SearchSettings", "FilterSetupFileType");
        }
    }
}
