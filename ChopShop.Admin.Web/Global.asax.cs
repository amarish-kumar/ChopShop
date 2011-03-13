using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using ChopShop.Admin.Web.Configuration;
using ChopShop.Configuration;
using ChopShop.Configuration.Admin;

namespace ChopShop.Admin.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class ChopShopAdminWeb : System.Web.HttpApplication
    {
        private static IWindsorContainer container;

        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            var routeMapper = new RouteMapper(routes);
            routeMapper.RegisterRoutes();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterContainer();
            RegisterFilterProviders();
            RegisterNHProfiler();
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