using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IProductService productService;

        public BasketController(IBasketService basketService, IProductService productService)
        {
            this.basketService = basketService;
            this.productService = productService;
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
            var shoppingBasket = Mapper.Map<Basket, ShoppingBasket>(basket);
            return Json(shoppingBasket);
        }

        [HttpGet]
        public ActionResult View (Basket basket)
        {
            var shoppingBasket = Mapper.Map<Basket, ShoppingBasket>(basket);
            var productIds = GetProductIdsFromBasket(shoppingBasket);
            var products = productService.GetProductsById(productIds);
            MapProductsToBasket(shoppingBasket, products);
            return View(shoppingBasket);
        }

        private void MapProductsToBasket(ShoppingBasket shoppingBasket, List<Product> products)
        {
            foreach (var item in shoppingBasket.BasketItems)
            {
                var product = products.FirstOrDefault(x => x.Id == item.ProductId);
                if (product == null) continue;
                item.Name = product.Name;
                item.Price = product.Prices.FirstOrDefault().Value;
                item.Currency = product.Prices.FirstOrDefault().Currency.ToString();
            }
        }

        private List<Guid> GetProductIdsFromBasket(ShoppingBasket shoppingBasket)
        {
            if(shoppingBasket != null && shoppingBasket.BasketItems.Any())
            {
                var ids = shoppingBasket.BasketItems.Select(item => item.ProductId).ToList();
                return ids;
            }
            return null;
        }
    }
}
