using System.Web.Optimization;

namespace OnlineAssessmentApplication.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Bootstrap").Include("~/Scripts/jquery-3.5.1.js", "~/Scripts/umd/popper.js", "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/Scripts/ClientSideValidationScripts").Include("~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.unobtrusive.js"));
            bundles.Add(new StyleBundle("~/Styles/Bootstrap").Include("~/Content/bootstrap.css", "~/Content/Site.css"));


            BundleTable.EnableOptimizations = true;
        }
    }
}