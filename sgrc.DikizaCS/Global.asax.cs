using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using sgrc.DikizaCS.App_Start;

namespace sgrc.DikizaCS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Web Api Attribute Routes
            //GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            // MVC Attribute Routes
            RouteTable.Routes.MapMvcAttributeRoutes();

            // Area Registration
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.EnsureInitialized();
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.UseXmlSerializer = true;

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

        }
    }
}
