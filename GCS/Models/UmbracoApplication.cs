using Umbraco.Core;
using W3S_GCS.App_Plugins.GCS.Models;
using W3S_GCS.App_Start;
using W3S_GCS.Installer;

namespace W3S_GCS.Models {
    public class UmbracoApplication : ApplicationEventHandler {
        public UmbracoApplication() {
        }
        protected override void ApplicationStarted(UmbracoApplicationBase httpApplication, ApplicationContext applicationContext) {
            base.ApplicationStarting(httpApplication, applicationContext);

            WebApiConfig.Register(System.Web.Http.GlobalConfiguration.Configuration);

            //db > entityframe migrations init

            //Database.SetInitializer<DBEntities>(new MigrateDatabaseToLatestVersion<DBEntities, W3S_GCS.Migrations.Configuration>());
            //using (DBEntities db = new DBEntities()) {
            //    db.Database.Initialize(true);
            //}

            //db > via package actions
            PackageActions.InitDatabase();

            LanguageInstaller.CheckAndInstallLanguageActions();
        }
    }
}