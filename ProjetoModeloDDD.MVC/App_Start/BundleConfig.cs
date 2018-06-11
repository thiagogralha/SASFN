using System.Web;
using System.Web.Optimization;

namespace SASF.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(
                    new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-*",
                        "~/Scripts/dirPagination.js",
                        "~/Scripts/mask.min.js"

            ));

            //FICHAS
            bundles.Add(
                   new ScriptBundle("~/bundles/angularFichas").Include(
                       "~/Scripts/app/fichas-controller.js",
                       "~/Scripts/app/validaCPF-utils.js",
                       "~/Scripts/js/FileSaver.min.js"
           ));

            //FUNCIONARIOS
            bundles.Add(
                   new ScriptBundle("~/bundles/angularFuncionarios").Include(
                       "~/Scripts/app/funcionarios-controller.js"
           ));

            //PLANOS
            bundles.Add(
                   new ScriptBundle("~/bundles/angularPlanos").Include(
                       "~/Scripts/app/planos-controller.js"
           ));

            //LOGIN
            bundles.Add(new StyleBundle("~/Login/css").Include(
                     "~/Content/util.css",
                     "~/Content/main.css",
                     "~/vendor/bootstrap/css/bootstrap.min.css",
                     "~/fonts/font-awesome-4.7.0/css/font-awesome.min.css",
                      "~/fonts/Linearicons-Free-v1.0.0/icon-font.min.css",
                      "~/vendor/animate/animate.css",
                      "~/vendor/css-hamburgers/hamburgers.min.css",
                      "~/vendor/animsition/css/animsition.min.css",
                      "~/vendor/select2/select2.min.css",
                      "~/vendor/daterangepicker/daterangepicker.css"
                     ));
            
            bundles.Add(
                   new ScriptBundle("~/Login/js").Include(
                       "~/Scripts/app/Login-controller.js"
           ));

            //TÍTULOS
            bundles.Add(
                   new ScriptBundle("~/bundles/angularTitulos").Include(
                       "~/Scripts/app/titulos-controller.js"
           ));


        }
    }
}
