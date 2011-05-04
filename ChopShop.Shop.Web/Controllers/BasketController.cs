using System;
using System.Web.Mvc;
using AutoMapper;
using ChopShop.Configuration;
using ChopShop.Model;
using ChopShop.Shop.Services.Interfaces;
using ChopShop.Shop.Web.Configuration.CustomFilters;
using ChopShop.Shop.Web.Models;

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
            var shoppingBasket = Mapper.Map<Basket, ShoppingBasket>(basket);
            return Json(shoppingBasket);
        }

        [HttpPost]
        public JsonResult _Remove(Guid productId, Basket basket)
        {
            basket.Remove(productId);
            basketService.Save(basket);
            return Json(basket);
        }

        [HttpGet]
        public ActionResult View (Basket basket)
        {
            var shoppingBasket = Mapper.Map<Basket, ShoppingBasket>(basket);
            return View(shoppingBasket);
        }

    }
}
