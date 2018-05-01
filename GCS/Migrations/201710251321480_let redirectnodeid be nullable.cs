namespace GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class letredirectnodeidbenullable : DbMigration {
        public override void Up() {
            AlterColumn("dbo.SearchSettings", "RedirectNodeId", c => c.Int());
        }

        public override void Down() {
            AlterColumn("dbo.SearchSettings", "RedirectNodeId", c => c.Int(nullable: false));
        }
    }
}
