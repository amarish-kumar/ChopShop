using System.Web.Mvc;
using System.Web.Routing;

namespace ChopShop.Admin.Web.Configuration
{
    public class RouteMapper
    {
        private readonly RouteCollection routes;

        public RouteMapper(RouteCollection routes)
        {
            this.routes = routes;
        }

        public void RegisterRoutes()
        {
            IgnoredRoutes();
            ProductRoutes();
            LogOnRoutes();
            FallbackRoute();
        }

        private void ProductRoutes()
        {
            routes.MapRoute(
                "DefaultProduct",
                "Product/",
                new { controller = "Product", action = "List", size=0, page = 0, orderyBy = string.Empty }
                );
        }

        private void IgnoredRoutes()
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }

        private void FallbackRoute()
        {
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        private void LogOnRoutes()
        {
            routes.MapRoute(
                "LogOn",
                "LogOn",
                new { controller = "Account", action = "LogOn" }
                );
        }
    }
}