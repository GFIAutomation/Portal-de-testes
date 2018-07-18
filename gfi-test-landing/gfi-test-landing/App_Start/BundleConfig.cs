﻿using System.Web;
using System.Web.Optimization;

namespace gfi_test_landing
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/js/vendor/-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/Content/js").Include(
                "~/Content/js/vendor/jquery-2.1.4.min.js",
                     "~/Content/js/plugins.js",
                     "~/Content/js/main.js",
                     "~/Content/js/dashboard.js",
                     "~/Content/js/widgets.js"));
           

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/normalize.css",
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/themify-icons.css",
                      "~/Content/css/flag-icon.min.css",
                      "~/Content/css/cs-skin-elastic.css",
                      "~/Content/scss/style.css"));
        }
    }
}
