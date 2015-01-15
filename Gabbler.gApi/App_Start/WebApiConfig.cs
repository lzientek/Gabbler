using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Gabbler.gApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuration et services API Web

            // Itinéraires de l'API Web
            config.MapHttpAttributeRoutes();
        }
    }
}
