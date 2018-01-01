using System.Web;
using System.Web.Optimization;

namespace SmpsPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app/services/attendanceService.js",
                 "~/Scripts/app/services/followService.js",
                  "~/Scripts/app/controllers/gigsController.js",
                   "~/Scripts/app/controllers/followController.js",
                   "~/Scripts/app/controllers/schoolRolesController.js",
                   "~/Scripts/app/controllers/assignCoursesController.js",
                   "~/Scripts/app/controllers/programsController.js",
                   "~/Scripts/app/controllers/LoadDropDownController.js",
                   "~/Scripts/app/controllers/gigDetailsController.js",
                "~/Scripts/app/app.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.unobtrusive*",
                       "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/underscore-min.js",
                     "~/Scripts/moment.js",
                     "~/Scripts/bootstrap.js",
                     "~/Scripts/bootstrapValidator.js",
                     "~/Scripts/respond.js",
                     "~/Scripts/toast.js",
                     "~/Scripts/multiselect.js",
                     "~/Content/assets/js/bootstrap-checkbox-radio-switch.js",
                     "~/Content/assets/js/chartist.min.js",
                     "~/Content/assets/js/bootstrap-notify.js",
                     "~/Content/assets/js/bootstrap-datepicker.js",
                      "~/Content/assets/js/light-bootstrap-dashboard.js",
                  
                     "~/Scripts/bootbox.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
         "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/ejscripts").Include(
                       "~/Scripts/jsrender.min.js",
                       "~/Scripts/jquery.easing-1.3.min.js",
                        "~/Scripts/ej/ej.web.all.min.js",
                        "~/Scripts/ej/ej.unobtrusive.min.js"));
            bundles.Add(new StyleBundle("~/bundles/ejstyles").Include(
                      "~/ejThemes/flat-saffron/ej.widgets.all.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrapValidator.css",
                      "~/Content/assets/css/light-bootstrap-dashboard.css",
                      "~/Content/assets/css/animate.min.css",
                      "~/Content/assets/css/pe-icon-7-stroke.css",
                     "~/Content/assets/css/bootstrap-datepicker.css",
                      "~/Content/ejgrid.responsive.css"));
        }
    }
}
