namespace GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class addfieldhtmlsnippet : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchEntries", "ClickTitle", c => c.String());
            AddColumn("dbo.SearchEntries", "HTMLSnippet", c => c.String());
            DropColumn("dbo.SearchEntries", "ClickNodeId");
        }

        public override void Down() {
            AddColumn("dbo.SearchEntries", "ClickNodeId", c => c.String());
            DropColumn("dbo.SearchEntries", "HTMLSnippet");
            DropColumn("dbo.SearchEntries", "ClickTitle");
        }
    }
}
