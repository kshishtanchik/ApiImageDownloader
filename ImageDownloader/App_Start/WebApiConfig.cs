using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ImageDownloader
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API
            //RouteTable.Routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}",
            //    defaults: new { controller = "Values", action = "GetImages" });

            //GlobalConfiguration.Configure(Register);

            //Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}",///{imageCount}/{threadCount}{url}
                defaults: new { controller = "Values" }//, imageCount = RouteParameter.Optional, threadCount = RouteParameter.Optional, url = RouteParameter.Optional } 
            );
        }
    }
}
