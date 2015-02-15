// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="">
//   Copyright © 2015 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.Gabbler.Web
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css/app").Include("~/content/app.css"));

            bundles.Add(new ScriptBundle("~/js/jquery").Include("~/scripts/vendor/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/js/app").Include(
                "~/scripts/vendor/angular-ui-router.js",
                "~/scripts/vendor/angular-local-storage.js",
                "~/scripts/app.js",
                "~/scripts/filters.js",
                "~/scripts/services/services.js",
                "~/scripts/services/authService.js",
                "~/scripts/services/gabService.js",
                "~/scripts/services/userServices.js",
                "~/scripts/services/authInterceptorService.js",
                "~/scripts/directives.js",
                "~/scripts/controllers/indexController.js",
                "~/scripts/controllers/gabControllers.js",
                "~/scripts/controllers/homeControllers.js",
                "~/scripts/controllers/userControllers.js"
                ));
        }
    }
}
