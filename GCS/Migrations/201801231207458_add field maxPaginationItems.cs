namespace GCS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfieldmaxPaginationItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SearchSettings", "MaxPaginationPages", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SearchSettings", "MaxPaginationPages");
        }
    }
}
