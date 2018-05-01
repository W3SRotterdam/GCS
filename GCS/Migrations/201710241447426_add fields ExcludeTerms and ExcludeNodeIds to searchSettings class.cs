namespace W3S_GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class addfieldsExcludeTermsandExcludeNodeIdstosearchSettingsclass : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchSettings", "ExcludeTerms", c => c.String());
            AddColumn("dbo.SearchSettings", "ExcludeNodeIds", c => c.String());
        }

        public override void Down() {
            DropColumn("dbo.SearchSettings", "ExcludeNodeIds");
            DropColumn("dbo.SearchSettings", "ExcludeTerms");
        }
    }
}
