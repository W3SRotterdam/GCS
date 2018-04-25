namespace GCS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfielddatecreatedandlastupdatedtosettingsclass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SearchSettings", "LastUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.SearchSettings", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SearchSettings", "DateCreated");
            DropColumn("dbo.SearchSettings", "LastUpdated");
        }
    }
}
