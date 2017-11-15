using System.Web.Mvc;
using System.Web.Routing;

namespace sgrc.DikizaCS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: "Account", url: "Register", defaults: new { controller = "Account", action = "Register" }, namespaces: new string[] { "sgrc.DikizaCS.Controllers" });
            routes.MapRoute(name: "Confirm", url: "Auth", defaults: new { controller = "Account", action = "Auth" }, namespaces: new string[] { "sgrc.DikizaCS.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
