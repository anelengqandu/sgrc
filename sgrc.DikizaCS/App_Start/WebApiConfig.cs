using System.Web.Http;
using sgrc.DikizaCS.Utility;

namespace sgrc.DikizaCS.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            GlobalConfiguration.Configuration.Filters.Add(new HttpExceptionFilterAttribute());
        }
    }
}