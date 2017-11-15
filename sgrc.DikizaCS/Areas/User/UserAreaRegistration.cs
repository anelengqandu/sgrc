using System.Web.Mvc;

namespace sgrc.DikizaCS.Areas.User
{
    public class UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "User";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    "User_default",
            //    "User/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            context.MapRoute(
               name: "User_default",
               url: "User/Dashboard/{controller}/{action}/{id}",
               defaults: new { controller = "Dashboard", action = "Dashboard", id = UrlParameter.Optional },
               namespaces: new string[] { "sgrc.DikizaCS.Areas.User.Controllers" }
           );
            context.MapRoute(
               name: "User_Programs_All",
               url: "User/Programs/{controller}/{action}/{id}",
               defaults: new { controller = "Program", action = "All", id = UrlParameter.Optional },
               namespaces: new string[] { "sgrc.DikizaCS.Areas.User.Controllers" }
           );
            context.MapRoute(
              name: "User_Programs_Edit",
              url: "User/Program/Edit/{controller}/{action}/{id}",
              defaults: new { controller = "Program", action = "Edit", id = UrlParameter.Optional },
              namespaces: new string[] { "sgrc.DikizaCS.Areas.User.Controllers" }
          );
            context.MapRoute(
               name: "User_Programs_Create",
               url: "User/Createprogram/{controller}/{action}/{id}",
               defaults: new { controller = "Program", action = "New", id = UrlParameter.Optional },
               namespaces: new string[] { "sgrc.DikizaCS.Areas.User.Controllers" }
           );


            context.MapRoute(
              name: "User_Logbook_All",
              url: "User/Logbooks/{controller}/{action}/{id}",
              defaults: new { controller = "Logbook", action = "All", id = UrlParameter.Optional },
              namespaces: new string[] { "sgrc.DikizaCS.Areas.User.Controllers" }
          );
            context.MapRoute(
              name: "User_Logbook_Edit",
              url: "User/Logbook/Edit/{controller}/{action}/{id}",
              defaults: new { controller = "Logbook", action = "Edit", id = UrlParameter.Optional },
              namespaces: new string[] { "sgrc.DikizaCS.Areas.User.Controllers" }
          );
            context.MapRoute(
               name: "User_Logbook_Create",
               url: "User/Createlogbook/{controller}/{action}/{id}",
               defaults: new { controller = "Logbook", action = "New", id = UrlParameter.Optional },
               namespaces: new string[] { "sgrc.DikizaCS.Areas.User.Controllers" }
           );



            context.MapRoute(
             name: "User_Client_All",
             url: "User/Clients/{controller}/{action}/{id}",
             defaults: new { controller = "Client", action = "All", id = UrlParameter.Optional },
             namespaces: new string[] { "sgrc.DikizaCS.Areas.User.Controllers" }
         );
            context.MapRoute(
              name: "User_Client_Edit",
              url: "User/Client/Edit/{controller}/{action}/{id}",
              defaults: new { controller = "Client", action = "Edit", id = UrlParameter.Optional },
              namespaces: new string[] { "sgrc.DikizaCS.Areas.User.Controllers" }
          );
            context.MapRoute(
               name: "User_Client_Create",
               url: "User/Createclient/{controller}/{action}/{id}",
               defaults: new { controller = "Client", action = "New", id = UrlParameter.Optional },
               namespaces: new string[] { "sgrc.DikizaCS.Areas.User.Controllers" }
           );

            context.MapRoute(
               name: "User_Student",
               url: "User/Student/Logbook/{controller}/{action}/{id}",
               defaults: new { controller = "Student", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "sgrc.DikizaCS.Areas.User.Controllers" }
           );

            context.MapRoute(
              name: "ClientUser_Index",
              url: "User/Client/{controller}/{action}/{id}",
              defaults: new { controller = "ClientUser", action = "Index", id = UrlParameter.Optional },
              namespaces: new string[] { "sgrc.DikizaCS.Areas.User.Controllers" }
          );
        }

    }
}