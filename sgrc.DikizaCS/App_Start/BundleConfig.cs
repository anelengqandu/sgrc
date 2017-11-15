using System.Web.Optimization;

namespace sgrc.DikizaCS.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            //CSS Theme
            bundles.Add(new StyleBundle("~/ThemePluginCSS").Include(
                 "~/Resources/theme/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                 "~/Resources/theme/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                 "~/Resources/theme/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                 "~/Resources/theme/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",

                 "~/Resources/theme/assets/global/plugins/select2/css/select2.min.css",
                 "~/Resources/theme/assets/global/plugins/select2/css/select2-bootstrap.min.css"
                 ));

            bundles.Add(new StyleBundle("~/GlobalThemeCSS").Include(
                "~/Resources/theme/assets/global/css/components.min.css",
                "~/Resources/theme/assets/global/css/plugins.min.css"
                ));

            bundles.Add(new StyleBundle("~/LoginThemeCSS").Include(
               "~/Resources/theme/assets/pages/css/login-5.min.css"
               ));



            bundles.Add(new ScriptBundle("~/ThemeJS").Include(
                     "~/Resources/theme/assets/global/plugins/jquery.min.js",
                        "~/Resources/theme/assets/global/plugins/jquery.min.js",
         "~/Resources/theme/assets/global/plugins/bootstrap/js/bootstrap.min.js",
         "~/Resources/theme/assets/global/plugins/js.cookie.min.js",
         "~/Resources/theme/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
         "~/Resources/theme/assets/global/plugins/jquery.blockui.min.js",
         "~/Resources/theme/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
          "~/Resources/theme/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
          "~/Resources/theme/assets/global/plugins/jquery-validation/js/additional-methods.min.js",
          "~/Resources/theme/assets/global/plugins/select2/js/select2.full.min.js",
          "~/Resources/theme/assets/global/plugins/backstretch/jquery.backstretch.min.js"
                     ));

            bundles.Add(new ScriptBundle("~/AppScriptJS").Include(
              "~/Resources/theme/assets/global/scripts/app.min.js"
              ));
           







            //*********************Start Page styles and scripts**************************************//
           bundles.Add(new ScriptBundle("~/LoginJS").Include(
             "~/scripts/_account/login.js"
             ));

            bundles.Add(new StyleBundle("~/LoginCSS").Include(
             "~/scripts/_account/loginCSS"
             ));
          
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}