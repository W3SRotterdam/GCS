namespace W3S_GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class addfieldsclickURlandclickNodeIdtosearchentryclass : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchEntries", "ClickURL", c => c.String());
            AddColumn("dbo.SearchEntries", "ClickNodeId", c => c.String());
        }

        public override void Down() {
            DropColumn("dbo.SearchEntries", "ClickNodeId");
            DropColumn("dbo.SearchEntries", "ClickURL");
        }
    }
}
