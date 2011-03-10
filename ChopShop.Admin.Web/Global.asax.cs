﻿using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ChopShop.Configuration;
using ChopShop.Configuration.Admin;

namespace ChopShop.Admin.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterContainer();
            RegisterFilterProviders();
            RegisterTypesInExecutingAssembly();
            RegisterNHProfiler();
        }

        private void RegisterTypesInExecutingAssembly()
        {
            container.Register(AllTypes.Pick()
                                   .From(Assembly.GetExecutingAssembly().GetTypes())
                                   .Configure(x => x.LifeStyle.PerWebRequest)
                                   .WithService.FirstInterface());
        }

        protected void Application_End()
        {
            container.Dispose();
        }

        private static void RegisterFilterProviders()
        {
            var oldProvider = FilterProviders.Providers.Single(x => x is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(oldProvider);

            var newProvider = new WindsorFilterAttributeProvider(container);
            FilterProviders.Providers.Add(newProvider);

        }

        private static void RegisterContainer()
        {
            container = new WindsorContainer().Install(new AdminServicesInstaller(), new ControllersInstaller());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        private static void RegisterNHProfiler()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }
    }
}