using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SOFT331_Assignment
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Timetable",
            //    url: "{controller}/{action}/{date}",
            //    defaults: new { controller = "Timetable", action = "Details", date = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Date",
                url: "{controller}/{action}/{year}/{month}/{day}",
                defaults: new { controller = "Journeys", action = "Timetable", year = UrlParameter.Optional, month = UrlParameter.Optional, day = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "DateTicket",
            //    url: "{controller}/{action}/{year}/{month}/{day}",
            //    defaults: new { controller = "Tickets", action = "Book", year = UrlParameter.Optional, month = UrlParameter.Optional, day = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "CreateTimetable",
            //    url: "{controller}/{action}/{year}/{month}/{day}",
            //    defaults: new { controller = "Timetable", action = "Create", year = UrlParameter.Optional, month = UrlParameter.Optional, day = UrlParameter.Optional }
            //);

            
        }
    }
}
