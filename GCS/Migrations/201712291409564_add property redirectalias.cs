namespace GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class addpropertyredirectalias : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchSettings", "RedirectAlias", c => c.String());
            DropColumn("dbo.SearchSettings", "RedirectNodeGUID");
        }

        public override void Down() {
            AddColumn("dbo.SearchSettings", "RedirectNodeGUID", c => c.String());
            DropColumn("dbo.SearchSettings", "RedirectAlias");
        }
    }
}
