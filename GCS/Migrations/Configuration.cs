namespace GCS.Migrations {
  using System.Data.Entity.Migrations;

  internal sealed class Configuration : DbMigrationsConfiguration<GCS.Models.DBEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GCS.Models.DBEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
