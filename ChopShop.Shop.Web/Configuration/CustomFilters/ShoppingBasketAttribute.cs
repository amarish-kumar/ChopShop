using System.Web.Mvc;
using ChopShop.Shop.Services.Interfaces;

namespace ChopShop.Shop.Web.Configuration.CustomFilters
{
    public class ShoppingBasketAttribute : ActionFilterAttribute
    {
        public IBasketService BasketService { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentBasket = BasketService.GetCurrentBasket();
            if (currentBasket != null)
            {
                filterContext.ActionParameters["basket"] = currentBasket;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}