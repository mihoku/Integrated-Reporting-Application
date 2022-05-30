using System.Web;
using System.Web.Optimization;

namespace ira
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.8.11.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/themes/base/jquery.ui.accordion.css",
                      "~/Content/themes/base/jquery.ui.all.css",
                      "~/Content/themes/base/jquery.ui.autocomplete.css",
                      "~/Content/themes/base/jquery.ui.base.css",
                      "~/Content/themes/base/jquery.ui.button.css",
                      "~/Content/themes/base/jquery.ui.core.css",
                      "~/Content/themes/base/jquery.ui.datepicker.css",
                      "~/Content/themes/base/jquery.ui.dialog.css",
                      "~/Content/themes/base/jquery.ui.progressbar.css",
                      "~/Content/themes/base/jquery.ui.resizable.css",
                      "~/Content/themes/base/jquery.ui.selectable.css",
                      "~/Content/themes/base/jquery.ui.slider.css",
                      "~/Content/themes/base/jquery.ui.tabs.css",
                      "~/Content/themes/base/jquery.ui.theme.css"
                      ));

            bundles.Add(new StyleBundle("~/Spectral/css").Include(
                "~/Scripts/spectral-layout/css/style.css",
                "~/Scripts/spectral-layout/css/font-awesome.min.css",
                      "~/Content/themes/base/jquery.ui.accordion.css",
                      "~/Content/themes/base/jquery.ui.all.css",
                      "~/Content/themes/base/jquery.ui.autocomplete.css",
                      "~/Content/themes/base/jquery.ui.base.css",
                      "~/Content/themes/base/jquery.ui.button.css",
                      "~/Content/themes/base/jquery.ui.core.css",
                      "~/Content/themes/base/jquery.ui.datepicker.css",
                      "~/Content/themes/base/jquery.ui.dialog.css",
                      "~/Content/themes/base/jquery.ui.progressbar.css",
                      "~/Content/themes/base/jquery.ui.resizable.css",
                      "~/Content/themes/base/jquery.ui.selectable.css",
                      "~/Content/themes/base/jquery.ui.slider.css",
                      "~/Content/themes/base/jquery.ui.tabs.css",
                      "~/Content/themes/base/jquery.ui.theme.css"
                ));

            bundles.Add(new ScriptBundle("~/Spectral/js").Include(
                "~/Scripts/spectral-layout/js/jquery.min.js",
                "~/Scripts/spectral-layout/js/jquery.scrollex.min.js",
                "~/Scripts/spectral-layout/js/jquery.scrolly.min.js",
                //"~/Scripts/jquery-ui-1.8.11.min.js",
                "~/Scripts/spectral-layout/js/skel.min.js",
                "~/Scripts/spectral-layout/js/init.js"
                //"~/Scripts/ira.js"
                ));

            bundles.Add(new StyleBundle("~/Dashgum/css").Include(
                "~/Scripts/spectral-layout/css/font-awesome.min.css",
                "~/Scripts/dashgum-layout/css/b*",
                    "~/Scripts/dashgum-layout/css/ui-button.css",
                "~/Scripts/dashgum-layout/css/style.css",
                "~/Scripts/dashgum-layout/css/t*",
                "~/Scripts/dashgum-layout/css/z*",
                "~/Scripts/dashgum-layout/js/nProgress/nprogress.css",
                "~/Scripts/dashgum-layout/js/fancybox/jquery.fancybox.css",
                "~/Scripts/dashgum-layout/js/gritter/css/jquery.gritter.css",
                "~/Scripts/dashgum-layout/js/animate/animate.css",
                //"~/Content/bootstrap.min.css",                      
                "~/Content/themes/base/jquery.ui.accordion.css",
                      "~/Content/themes/base/jquery.ui.all.css",
                      "~/Content/themes/base/jquery.ui.autocomplete.css",
                      "~/Content/themes/base/jquery.ui.base.css",
                      "~/Content/themes/base/jquery.ui.button.css",
                      "~/Content/themes/base/jquery.ui.core.css",
                      "~/Content/themes/base/jquery.ui.datepicker.css",
                      "~/Content/themes/base/jquery-ui-timepicker-addon.min.css",
                      "~/Content/themes/base/jquery.ui.dialog.css",
                      "~/Content/themes/base/jquery.ui.progressbar.css",
                      "~/Content/themes/base/jquery.ui.resizable.css",
                      "~/Content/themes/base/jquery.ui.selectable.css",
                      "~/Content/themes/base/jquery.ui.slider.css",
                      "~/Content/themes/base/jquery.ui.tabs.css",
                      "~/Content/themes/base/jquery.ui.theme.css",
                      "~/Scripts/dashgum-layout/js/data-tables/css/jquery.dataTables.min.css",
                      "~/Scripts/dashgum-layout/js/data-tables/css/dataTables.responsive.css",
                      //"~/Scripts/dashgum-layout/js/data-tables/css/dataTables.foundation.css",
                      "~/Scripts/dashgum-layout/js/Toastr/toastr.min.css"
                      //"~/Scripts/materialize/css/materialize.min.css"
                ));

            bundles.Add(new StyleBundle("~/Dashgum/flash").Include(
    "~/Scripts/spectral-layout/css/font-awesome.min.css",
    "~/Scripts/dashgum-layout/css/ui-button.css",
    "~/Scripts/dashgum-layout/css/b*",
    "~/Scripts/dashgum-layout/css/styleflash.css",
    "~/Scripts/dashgum-layout/css/t*",
    "~/Scripts/dashgum-layout/css/z*",
    "~/Scripts/dashgum-layout/js/nProgress/nprogress.css",
    "~/Scripts/dashgum-layout/js/fancybox/jquery.fancybox.css",
    "~/Scripts/dashgum-layout/js/gritter/css/jquery.gritter.css",
    "~/Scripts/dashgum-layout/js/animate/animate.css",
                //"~/Content/bootstrap.min.css",                      
    "~/Content/themes/base/jquery.ui.accordion.css",
          "~/Content/themes/base/jquery.ui.all.css",
          "~/Content/themes/base/jquery.ui.autocomplete.css",
          "~/Content/themes/base/jquery.ui.base.css",
          "~/Content/themes/base/jquery.ui.button.css",
          "~/Content/themes/base/jquery.ui.core.css",
          "~/Content/themes/base/jquery.ui.datepicker.css",
          "~/Content/themes/base/jquery-ui-timepicker-addon.min.css",
          "~/Content/themes/base/jquery.ui.dialog.css",
          "~/Content/themes/base/jquery.ui.progressbar.css",
          "~/Content/themes/base/jquery.ui.resizable.css",
          "~/Content/themes/base/jquery.ui.selectable.css",
          "~/Content/themes/base/jquery.ui.slider.css",
          "~/Content/themes/base/jquery.ui.tabs.css",
          "~/Content/themes/base/jquery.ui.theme.css",
          "~/Scripts/dashgum-layout/js/data-tables/css/jquery.dataTables.min.css",
          "~/Scripts/dashgum-layout/js/data-tables/css/dataTables.responsive.css",
                //"~/Scripts/dashgum-layout/js/data-tables/css/dataTables.foundation.css",
          "~/Scripts/dashgum-layout/js/Toastr/toastr.min.css"
                //"~/Scripts/materialize/css/materialize.min.css"
    ));
            
            bundles.Add(new ScriptBundle("~/Dashgum/js").Include(
                //"~/Scripts/dashgum-layout/js/jquery*",
                //"~/Scripts/jquery-{version}.js",
                "~/Scripts/dashgum-layout/js/jquery-1.8.3.min.js",
                "~/Scripts/dashgum-layout/js/nProgress/nprogress.js",
                //"~/Scripts/jquery-{version}.js",
                "~/Scripts/dashgum-layout/js/jquery-ui-1.9.2.custom.min.js",
                "~/Scripts/dashgum-layout/js/jquery-ui-timepicker-addon.min.js",
                "~/Scripts/dashgum-layout/js/jquery-ui-sliderAccess.js",
                "~/Scripts/dashgum-layout/js/bootstrap.min.js",
                "~/Scripts/dashgum-layout/js/jquery.scrollTo.min.js",
                "~/Scripts/dashgum-layout/js/jquery.nicescroll.js",
                //"~/Scripts/jquery-ui-1.8.11.min.js",
                //"~/Scripts/materialize/js/materialize.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/jquery.validate-vsdoc.js",
                //"~/Scripts/jquery-1.10.2.intellisense.js",
                "~/Scripts/dashgum-layout/js/bootstrap.min.js",
                "~/Scripts/dashgum-layout/js/bootstrap-switch.js",
                "~/Scripts/dashgum-layout/js/jquery.backstretch.min.js",
                "~/Scripts/dashgum-layout/js/jquery.scrollTo.min.js",
                "~/Scripts/dashgum-layout/js/jquery.ui.touch-punch.min.js",
                "~/Scripts/dashgum-layout/js/jquery.dcjqaccordion.2.7.js",
                "~/Scripts/dashgum-layout/js/common-scripts.js",
                //"~/Scripts/dashgum-layout/js/c*",
                //"~/Scripts/dashgum-layout/js/e*",
                //"~/Scripts/dashgum-layout/js/f*",
                "~/Scripts/dashgum-layout/js/fancybox/jquery.fancybox.js",
                "~/Scripts/dashgum-layout/js/gritter/js/jquery.gritter.js",
                "~/Scripts/dashgum-layout/js/g*",
                //"~/Scripts/dashgum-layout/js/m*",
                //"~/Scripts/dashgum-layout/js/s*",
                "~/Scripts/dashgum-layout/js/t*",
                "~/Scripts/dashgum-layout/js/z*",
                "~/Scripts/dashgum-layout/js/data-tables/js/jquery.dataTables.min.js",
                "~/Scripts/dashgum-layout/js/data-tables/js/dataTables.responsive.js",
                //"~/Scripts/dashgum-layout/js/data-tables/js/dataTables.foundation.js",
                "~/Scripts/ira.js",
                "~/Scripts/dashgum-layout/js/Toastr/toastr.min.js",
                "~/Scripts/wysihtml5/wysihtml5-0.3.0.js",
                "~/Scripts/wysihtml5/bootstrap-wysihtml5.js",
                "~/Scripts/dashgum-layout/js/raphael/raphael.min.js",
                "~/Scripts/dashgum-layout/js/morris/morris.min.js",
                "~/Scripts/canvasjs/canvasjs.min.js"
                ));
        }
    }
}
