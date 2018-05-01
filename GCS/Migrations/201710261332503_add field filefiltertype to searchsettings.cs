namespace GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class addfieldfilefiltertypetosearchsettings : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchSettings", "ShowFilterFileType", c => c.Boolean(nullable: false));
        }

        public override void Down() {
            DropColumn("dbo.SearchSettings", "ShowFilterFileType");
        }
    }
}
