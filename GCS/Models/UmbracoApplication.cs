using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Services.Implement;
using W3S_GCS.App_Plugins.GCS.Models;
using W3S_GCS.App_Start;
using W3S_GCS.Installer;

namespace W3S_GCS.Models {
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class UmbracoApplication : IUserComposer {

        public void Compose(Composition composition) {
            composition.Components().Append<GCSComponent>();
        }

        public class GCSComponent : IComponent
        {
            public void Initialize() {
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

            public void Terminate() {
                throw new System.NotImplementedException();
            }
        }
    }
}