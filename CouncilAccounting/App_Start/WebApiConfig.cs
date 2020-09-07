﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;


namespace CouncilAccounting
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            //var cors = new EnableCorsAttribute("https://municipalcouncil.councilaccounting.com", "*", "*");
            // var cors = new EnableCorsAttribute("https://accountingcouncil.transmetrics.co.in", "*", "*");
            var cors = new EnableCorsAttribute("https://warora.councilaccounting.in", "*", "*");

            // var cors = new EnableCorsAttribute("https://councilaccounting.in/", "*", "*");
           //var cors = new EnableCorsAttribute("https://parseoni.councilaccounting.in", "*", "*");
            //var cors = new EnableCorsAttribute("https://wanadongri.councilaccounting.in", "*", "*");
         //  var cors = new EnableCorsAttribute("https://parseoni1.councilaccounting.in", "*", "*");
            config.EnableCors(cors);
            //config.EnableCors(cors1);
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }
    }
}
