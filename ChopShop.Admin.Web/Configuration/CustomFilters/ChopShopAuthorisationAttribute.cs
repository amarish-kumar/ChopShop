using System.Web;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;

namespace ChopShop.Admin.Web.Configuration.CustomFilters
{
    public class ChopShopAuthorisationAttribute : AuthorizeAttribute
    {
        public IAdminAuthenticationService AuthenticationService { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session == null || httpContext.Session["adminUser"] == null) // if the session has died then kill forms authentication
            {
                AuthenticationService.SignOut(httpContext.Session);
                return false;
            }
            return base.AuthorizeCore(httpContext);
        }
    }
}