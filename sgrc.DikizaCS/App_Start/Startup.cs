using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace sgrc.DikizaCS
{
    public partial class Startup
    {
        public void ConfigureAuthentication(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/"),
                CookieName = "sgrc.DikizaCS.auth"
                //CookieSecure = CookieSecureOption.Always
            });
        }
    }
}