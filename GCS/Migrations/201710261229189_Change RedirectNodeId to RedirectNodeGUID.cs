namespace GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class ChangeRedirectNodeIdtoRedirectNodeGUID : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchSettings", "RedirectNodeGUID", c => c.String());
            DropColumn("dbo.SearchSettings", "RedirectNodeId");
        }

        public override void Down() {
            AddColumn("dbo.SearchSettings", "RedirectNodeId", c => c.Int());
            DropColumn("dbo.SearchSettings", "RedirectNodeGUID");
        }
    }
}
