using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace sgrc.DikizaCS.Utility
{
    public static class UserSession
    {
        public static string LoggedInEmail(this Controller c)
        {
            var ctx = c.HttpContext.GetOwinContext();
            var email = ctx.Authentication.User.Claims.Where(cl => cl.Type == ClaimTypes.Email).First().Value;
            return email;
        }

        public static string LoggedInSecurityStamp(this Controller c)
        {
            var ctx = c.HttpContext.GetOwinContext();
            var securityStamp = ctx.Authentication.User.Claims.Where(cl => cl.Type == "securityStamp").First().Value;
            return securityStamp;
        }

        public static string LoggedInCompanyName(this Controller c)
        {
            var ctx = c.HttpContext.GetOwinContext();
            var companyName = ctx.Authentication.User.Claims.Where(cl => cl.Type == "CompanyName").First().Value;
            return companyName;
        }

        public static string LoggedInCompanyContactEmail(this Controller c)
        {
            var ctx = c.HttpContext.GetOwinContext();
            var companyContactEmail = ctx.Authentication.User.Claims.Where(cl => cl.Type == "ContactEmail").First().Value;
            return companyContactEmail;
        }

        public static long LoggedInId(this Controller c)
        {
            var ctx = c.HttpContext.GetOwinContext();
            var id = ctx.Authentication.User.Claims.Where(cl => cl.Type == ClaimTypes.NameIdentifier).First().Value;
            return long.Parse(id);
        }

        public static long LoggedInStudentClientId(this Controller c)
        {
            var ctx = c.HttpContext.GetOwinContext();
            var id = ctx.Authentication.User.Claims.Where(cl => cl.Type == "ClientId").First().Value;
            return long.Parse(id);
        }

        public static long LoggedInStudentProgramId(this Controller c)
        {
            var ctx = c.HttpContext.GetOwinContext();
            var id = ctx.Authentication.User.Claims.Where(cl => cl.Type == "ProgramId").First().Value;
            return long.Parse(id);
        }

        public static int ProgramCounter(this Controller c)
        {
            var ctx = c.HttpContext.GetOwinContext();
            var programsCounter = ctx.Authentication.User.Claims.Where(cl => cl.Type == "ProgramsCounter").First().Value;
            return int.Parse(programsCounter);
        }

        public static int ClientCounter(this Controller c)
        {
            var ctx = c.HttpContext.GetOwinContext();
            var clientCounter = ctx.Authentication.User.Claims.Where(cl => cl.Type == "ClientCounter").First().Value;
            return int.Parse(clientCounter);
        }

        public static string LoggedInName(this Controller c)
        {
            var ctx = c.HttpContext.GetOwinContext();
            var fullname = ctx.Authentication.User.Claims.Where(cl => cl.Type == ClaimTypes.Name).First().Value;
            return fullname;
        }

        public static bool LoggedInIsAdmin(this Controller c)
        {
            var ctx = c.HttpContext.GetOwinContext();
            var IsAdmin = ctx.Authentication.User.Claims.Where(cl => cl.Type == ClaimTypes.Role).First().Value;
            return bool.Parse(IsAdmin);
        }

        /* ------------------------------------------------- */
        // Auth API
        /* ------------------------------------------------- */
        public static long LoggedInId(this ApiController c)
        {
            var ctx = HttpContext.Current.GetOwinContext();
            var id = ctx.Authentication.User.Claims.Where(cl => cl.Type == ClaimTypes.NameIdentifier).First().Value;
            return long.Parse(id);
        }
        public static string LoggedInSecurityStamp(this ApiController c)
        {
            var ctx = HttpContext.Current.GetOwinContext();
            var securityStamp = ctx.Authentication.User.Claims.Where(cl => cl.Type == "securityStamp").First().Value;
            return securityStamp;
        }

        public static int ProgramCounter(this ApiController c)
        {
            var ctx = HttpContext.Current.GetOwinContext();
            var programsCounter = ctx.Authentication.User.Claims.Where(cl => cl.Type == "ProgramsCounter").First().Value;
            return int.Parse(programsCounter);
        }

        public static long LoggedInStudentClientId(this ApiController c)
        {
            var ctx = HttpContext.Current.GetOwinContext();
            var id = ctx.Authentication.User.Claims.Where(cl => cl.Type == "ClientId").First().Value;
            return long.Parse(id);
        }

        public static long LoggedInStudentProgramId(this ApiController c)
        {
            var ctx = HttpContext.Current.GetOwinContext();
            var id = ctx.Authentication.User.Claims.Where(cl => cl.Type == "ProgramId").First().Value;
            return long.Parse(id);
        }

        public static string LoggedInCompanyName(this ApiController c)
        {
            var ctx = HttpContext.Current.GetOwinContext();
            var companyName = ctx.Authentication.User.Claims.Where(cl => cl.Type == "CompanyName").First().Value;
            return companyName;
        }

        public static string LoggedInCompanyContactEmail(this ApiController c)
        {
            var ctx = HttpContext.Current.GetOwinContext();
            var companyContactEmail = ctx.Authentication.User.Claims.Where(cl => cl.Type == "ContactEmail").First().Value;
            return companyContactEmail;
        }

        public static int ClientCounter(this ApiController c)
        {
            var ctx = HttpContext.Current.GetOwinContext();
            var clientCounter = ctx.Authentication.User.Claims.Where(cl => cl.Type == "ClientCounter").First().Value;
            return int.Parse(clientCounter);
        }

    }
}