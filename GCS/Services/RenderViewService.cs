using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace W3S_GCS.Services {
    public class RenderViewService {
        public static string RenderView(Controller controller, string action) {
            using (StringWriter sw = new StringWriter()) {
                ViewEngineResult vur = ViewEngines.Engines.FindPartialView(controller.ControllerContext, action);
                ViewContext context = new ViewContext(controller.ControllerContext, vur.View, controller.ViewData, controller.TempData, sw);
                vur.View.Render(context, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
        public static string RenderView(Controller controller, string action, object model) {
            controller.ViewData.Model = model;
            using (StringWriter sw = new StringWriter()) {
                ViewEngineResult vur = ViewEngines.Engines.FindPartialView(controller.ControllerContext, action);
                ViewContext context = new ViewContext(controller.ControllerContext, vur.View, controller.ViewData, controller.TempData, sw);
                vur.View.Render(context, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

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