using System.Collections.Generic;
using System.Web.Mvc;
using ChopShop.Admin.Web.Configuration.CustomFilters;
using ChopShop.Admin.Web.Models;

namespace ChopShop.Admin.Web.Controllers
{
    [ChopShopAuthorisation]
    [GetAdminSession]
    public class ChopShopController : Controller
    {
        protected void AddModelStateErrors(List<ErrorInfo> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
        }
    }
}