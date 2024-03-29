﻿using System;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Gabbler.gApi.Startup))]

namespace Gabbler.gApi
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            ConfigureOAuth(appBuilder);
            // Configure Web API 
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.UseWebApi(config);
            

        }

        /// <summary>
        /// configuration de l'authentification
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            //todo: faire marcher
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

    }
}