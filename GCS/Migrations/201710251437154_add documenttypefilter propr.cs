namespace GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class adddocumenttypefilterpropr : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchSettings", "DocumentTypeFilter", c => c.String());
            AddColumn("dbo.SearchSettings", "DateRestrict", c => c.DateTime());
        }

        public override void Down() {
            DropColumn("dbo.SearchSettings", "DateRestrict");
            DropColumn("dbo.SearchSettings", "DocumentTypeFilter");
        }
    }
}
