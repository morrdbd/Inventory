using System.Web;
using System.Web.Optimization;

namespace DOMoRR.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var cssRewrite = new CssRewriteUrlTransform();
            //core js
            bundles.Add(new ScriptBundle("~/bundles/corejs").Include(
                "~/Content/assets/global/plugins/jquery.min.js",
                "~/Content/assets/global/plugins/jquery-migrate.min.js",
                "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Content/assets/global/plugins/jquery.blockui.min.js",
                "~/Content/assets/global/plugins/jquery.cokie.min.js",
                "~/Content/assets/global/scripts/metronic.js",
                "~/Content/assets/admin/layout/scripts/layout.js",
                "~/Content/assets/admin/layout/scripts/demo.js",
                "~/Content/assets/global/plugins/bootstrap-confirmation/bootstrap-confirmation.min.js",
                "~/Content/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
                "~/Content/resources/libs/jquery-validate.bootstrap-tooltip.min.js",
                "~/Content/assets/global/plugins/bootbox/bootbox.min.js",
                "~/Content/resources/libs/jquery.form.min.js",
                "~/Content/resources/libs/jquery.serializejson.js",
                "~/Content/resources/libs/sprintf.min.js",
                "~/Content/assets/admin/pages/scripts/components-pickers.js",
                "~/Content/assets/global/plugins/uniform/jquery.uniform.min.js",
                "~/Content/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
                "~/Content/assets/global/plugins/bootstrap-toastr/toastr.min.js",
                "~/Content/resources/datatable/datatables.min.js"
                ));

            //core css
            bundles.Add(new StyleBundle("~/bundles/corecss")
                .Include("~/Content/resources/src/layout.css", cssRewrite)
                .Include("~/Content/assets/global/plugins/bootstrap-datepicker/css/datepicker3.css", cssRewrite)
                .Include("~/Content/resources/ui-datepicker/themes/black-tie/jquery-ui.min.css", cssRewrite)
                .Include("~/Content/assets/frontend/pages/css/portfolio.css", cssRewrite)
                .Include("~/Content/assets/global/plugins/uniform/css/uniform.default.min.css", cssRewrite)
                .Include("~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css", cssRewrite)
                .Include("~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css", cssRewrite));

            //rtl css
            bundles.Add(new StyleBundle("~/bundles/cssrtl")
                .Include("~/Content/assets_rtl/global/plugins/bootstrap/css/bootstrap-rtl.min.css", cssRewrite)
                .Include("~/Content/assets_rtl/admin/pages/css/error-rtl.css", cssRewrite)
                .Include("~/Content/assets_rtl/global/css/plugins-md-rtl.css", cssRewrite)
                .Include("~/Content/assets_rtl/admin/layout/css/layout-rtl.css", cssRewrite)
                .Include("~/Content/assets_rtl/admin/layout/css/custom-rtl.css", cssRewrite)
                .Include("~/Content/assets_rtl/global/plugins/bootstrap-toastr/toastr-rtl.min.css")
                .Include("~/Content/resources/fontcss/noto.css", cssRewrite)
                .Include("~/Content/assets_rtl/global/css/components-md-rtl.css", cssRewrite)
                .Include("~/Content/assets_rtl/admin/layout/css/themes/darkblue-rtl.css", cssRewrite));

            //ltr css
            bundles.Add(new StyleBundle("~/bundles/cssltr")
                .Include("~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css", cssRewrite)
                .Include("~/Content/assets/admin/pages/css/error.css", cssRewrite)
                .Include("~/Content/assets/global/css/plugins-md.css", cssRewrite)
                .Include("~/Content/assets/admin/layout/css/layout.css", cssRewrite)
                .Include("~/Content/assets/admin/layout/css/custom.css", cssRewrite)
                .Include("~/Content/assets/global/plugins/bootstrap-toastr/toastr.min.css")
                .Include("~/Content/resources/fontcss/fonts.css", cssRewrite)
                .Include("~/Content/assets/global/css/components-md.css", cssRewrite)
                .Include("~/Content/assets/admin/layout/css/themes/darkblue.css", cssRewrite));

            bundles.Add(new StyleBundle("~/bundles/customecss").Include(
                      "~/Content/resources/custom.css"
                      ));

            //highcharts
            bundles.Add(new ScriptBundle("~/bundles/highcharts").Include(
                "~/Content/resources/highcharts/highcharts.js",
                "~/Content/resources/highcharts/highcharts-3d.js",
                "~/Content/resources/highcharts/exporting.js",
                "~/Content/resources/highcharts/offline-exporting.js",
                "~/Content/resources/highcharts/export-helper.js"));

            //home page js
            bundles.Add(new ScriptBundle("~/bundles/homejs").Include(
                "~/Content/assets/global/plugins/jquery.min.js",
                "~/Content/assets/global/plugins/jquery-migrate.min.js",
                "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                "~/Content/assets/frontend/layout/scripts/back-to-top.js",
                "~/Content/assets/global/plugins/uniform/jquery.uniform.min.js",
                "~/Content/assets/frontend/layout/scripts/layout.js",
                "~/Content/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
                "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Content/resources/libs/jquery.cookie.js"
                ,"~/Content/resources/libs/angular.min.js"
                ));

            bundles.FileSetOrderList.Clear();
            BundleTable.EnableOptimizations = true;
        }
    }
}