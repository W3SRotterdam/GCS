using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace W3S_GCS.Services {
    public class RenderViewService {
        public static string GetRazorViewAsString(object model, string filePath) {
            StringWriter stringWriter = new StringWriter();
            HttpContextWrapper context = new HttpContextWrapper(HttpContext.Current);
            RouteData routeData = new RouteData();
            ControllerContext controllerContext = new ControllerContext(new RequestContext(context, routeData), new DummyController());
            RazorView razor = new RazorView(controllerContext, filePath, null, false, null);
            razor.Render(new ViewContext(controllerContext, razor, new ViewDataDictionary(model), new TempDataDictionary(), stringWriter), stringWriter);
            return stringWriter.ToString();
        }

        public class DummyController : Controller { }
    }
}