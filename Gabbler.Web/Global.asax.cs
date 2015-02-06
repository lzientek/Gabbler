// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="">
//   Copyright © 2015 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Optimization;

namespace App.Gabbler.Web
{
    using System.Web;
    using System.Web.Routing;

    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
