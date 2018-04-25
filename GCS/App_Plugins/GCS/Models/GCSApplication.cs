using umbraco.businesslogic;
using umbraco.interfaces;

namespace GCS.App_Plugins.GCS.Models {
    public class GCSApplication {
        [Application("GCS", "GCS", "icon-binoculars", 20)]
        public class GoogleSearchApplication : IApplication {
        }
    }
}