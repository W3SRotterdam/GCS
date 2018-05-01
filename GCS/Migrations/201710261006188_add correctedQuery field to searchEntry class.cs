namespace W3S_GCS.Migrations {
    using System.Data.Entity.Migrations;

    public partial class addcorrectedQueryfieldtosearchEntryclass : DbMigration {
        public override void Up() {
            AddColumn("dbo.SearchEntries", "CorrectedQuery", c => c.String());
        }

        public override void Down() {
            DropColumn("dbo.SearchEntries", "CorrectedQuery");
        }
    }
}
