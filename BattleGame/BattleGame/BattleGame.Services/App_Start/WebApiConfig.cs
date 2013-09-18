using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BattleGame.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "UnitsApi",
                routeTemplate: "api/units/{id}/{action}",
                 defaults: new { controller = "units" }
            );

            config.Routes.MapHttpRoute(
                name: "HeroApi",
                routeTemplate: "api/heroes/{id}/units",
                 defaults: new { controller = "heroes" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}"
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=301869.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
