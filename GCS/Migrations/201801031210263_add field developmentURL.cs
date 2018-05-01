namespace W3S_GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class addfielddevelopmentURL : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchSettings", "DevelopmentURL", c => c.String());
        }

        public override void Down() {
            DropColumn("dbo.SearchSettings", "DevelopmentURL");
        }
    }
}
