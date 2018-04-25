namespace GCS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfiltersetuptosearchsettingsclass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SearchSettings", "FilterSetup", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SearchSettings", "FilterSetup");
        }
    }
}
