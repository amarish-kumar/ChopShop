using System.Web.Mvc;

namespace ChopShop.Admin.Web.Configuration.CustomFilters
{
    public class GetAdminSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                if (filterContext.HttpContext.Session["adminUser"] != null)
                {
                    filterContext.ActionParameters["adminUser"] = filterContext.HttpContext.Session["adminUser"];
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}