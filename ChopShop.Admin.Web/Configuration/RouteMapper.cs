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

            FallbackRoute();
        }

        private void ProductRoutes()
        {
            routes.MapRoute(
                "DefaultProduct",
                "Product/",
                new { controller = "Product", action = "List" }
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
    }
}