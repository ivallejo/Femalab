using System.Web;
using System.Web.Optimization;

namespace Femalab
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/patient-custom-validator").Include(
                        "~/Scripts/Patient/script-custom-validator.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/admintablescss").Include(
                    "~/Content/vendor/datatables/dataTables.bootstrap4.css"));

            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                   "~/Content/sb-admin-2.css",
                   "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/adminfont").Include(
                   "~/Content/vendor/fontawesome-free/all.css"));

            bundles.Add(new ScriptBundle("~/bundles/adminjs").Include(
                     "~/Scripts/vendor/jquery.js",
                     "~/Scripts/vendor/boostrap/bootstrap.bundle.js",
                     "~/Scripts/sb-admin-2.js"));

            bundles.Add(new ScriptBundle("~/bundles/adminpuglin").Include(
                     "~/Scripts/vendor/jquery.easing.js"));

            bundles.Add(new ScriptBundle("~/bundles/admintablesjs").Include(
                "~/Scripts/vendor/datatables/jquery.dataTables.js",
                "~/Scripts/vendor/datatables/dataTables.bootstrap4.js"));

            bundles.Add(new ScriptBundle("~/bundles/Patient").Include(
               "~/Scripts/patient/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/attention").Include(
               "~/Scripts/attention/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/Product").Include(
               "~/Scripts/product/index.js"));


            bundles.Add(new ScriptBundle("~/bundles/attentionquery").Include(
               "~/Scripts/attention/attention.js"));

            bundles.Add(new ScriptBundle("~/bundles/laboratoryquery").Include(
               "~/Scripts/attention/laboratory.js"));

            bundles.Add(new ScriptBundle("~/Content/laboratorycss").Include(
                      "~/Content/css-easy-autocomplete/easy-autocomplete.min.css"));

            bundles.Add(new ScriptBundle("~/Content/toas").Include(
                       "~/Content/toastr.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/toas").Include(
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/notifier.js"));

            bundles.Add(new ScriptBundle("~/bundles/autocomplete").Include(
                     "~/Scripts/jquery-easy-autocomplete/jquery.easy-autocomplete.min"));

        }
    }
}
