using System.Web.Http;

namespace GCS.App_Start {
    public class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            config.Routes.MapHttpRoute(
                name: "GCS",
                routeTemplate: "umbraco/backoffice/gcs/{controller}/{action}"
            );
        }
    }
}