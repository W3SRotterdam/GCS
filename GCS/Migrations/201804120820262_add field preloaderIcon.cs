namespace GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class addfieldpreloaderIcon : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchSettings", "LoadIconGUID", c => c.String());
        }

        public override void Down() {
            DropColumn("dbo.SearchSettings", "LoadIconGUID");
        }
    }
}
