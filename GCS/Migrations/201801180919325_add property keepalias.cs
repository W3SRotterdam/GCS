namespace W3S_GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class addpropertykeepalias : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchSettings", "KeepQuery", c => c.Boolean(nullable: false));
        }

        public override void Down() {
            DropColumn("dbo.SearchSettings", "KeepQuery");
        }
    }
}
