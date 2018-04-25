namespace GCS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makefieldsdatecreatedandupdatedatenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SearchSettings", "LastUpdated", c => c.DateTime());
            AlterColumn("dbo.SearchSettings", "DateCreated", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SearchSettings", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SearchSettings", "LastUpdated", c => c.DateTime(nullable: false));
        }
    }
}
