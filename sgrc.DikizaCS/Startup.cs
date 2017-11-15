using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(sgrc.DikizaCS.Startup))]


namespace sgrc.DikizaCS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuthentication(app);
        }
    }
}