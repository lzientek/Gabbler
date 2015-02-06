// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="">
//   Copyright © 2015 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.Gabbler.Web
{
    using System.Web.Routing;

    using App.Gabbler.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add("Default", new DefaultRoute());
        }
    }
}
