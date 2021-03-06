﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.OData.Batch;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.Http.Cors;

namespace EF.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/take/{action}" //"api/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional } //action = "DefaultAction", 
            );

            //global OData enabling
            //config.EnableQuerySupport();
            System.Web.OData.Extensions.HttpConfigurationExtensions.AddODataQueryFilter(config);

            //excluding circular links in JSON (if present)
            //1
            /*
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = 
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                */
            //2
            /*
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = 
            Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = 
            Newtonsoft.Json.PreserveReferencesHandling.Objects;
            */
        }
    }
}
