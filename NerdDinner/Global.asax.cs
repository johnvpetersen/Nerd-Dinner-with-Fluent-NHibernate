﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NerdDinner.Models;
using NHibernate;

namespace NerdDinner {

    public class MvcApplication : System.Web.HttpApplication {

        public void RegisterRoutes(RouteCollection routes) {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "UpcomingDinners", 
                "Dinners/Page/{page}", 
                new { controller = "Dinners", action = "Index" }
            );

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
        }

        void Application_Start() {

            Application["SessionFactory"] = new SessionFactory().CreateSessionFactory();

            RegisterRoutes(RouteTable.Routes);


        }
    }
}