using System;
using System.Web.Mvc;
using ChopShop.Configuration;
using ChopShop.Model;
using ChopShop.Shop.Services.Interfaces;
using ChopShop.Shop.Web.Configuration.CustomFilters;

namespace ChopShop.Shop.Web.Controllers
{
    [ShoppingBasket]
    [TransactionFilter(TransactionFilterType.ReadCommitted)]
    public class BasketController : Controller
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        [HttpPost]
        public JsonResult _Add (Guid productId, int quantity, Basket basket)
        {
            basket.Add(productId, quantity);
            basketService.Save(basket);
            return Json(basket);
        }

        [HttpPost]
        public JsonResult _Remove(Guid productId, Basket basket)
        {
            basket.Remove(productId);
            basketService.Save(basket);
            return Json(basket);
        }

    }
}
