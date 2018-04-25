using GCS.App_Plugins.GCS.Models;
using GCS.App_Start;
using System.Data.Entity;
using Umbraco.Core;

namespace GCS.Models {
    public class UmbracoApplication : ApplicationEventHandler {
        public UmbracoApplication() {
        }
        protected override void ApplicationStarted(UmbracoApplicationBase httpApplication, ApplicationContext applicationContext) {
            base.ApplicationStarting(httpApplication, applicationContext);

            WebApiConfig.Register(System.Web.Http.GlobalConfiguration.Configuration);
            Database.SetInitializer<DBEntities>(new MigrateDatabaseToLatestVersion<DBEntities, GCS.Migrations.Configuration>());
            using (DBEntities db = new DBEntities()) {
                db.Database.Initialize(true);
            }

            LanguageInstaller.CheckAndInstallLanguageActions();
        }
    }
}