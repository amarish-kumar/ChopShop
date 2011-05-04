using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChopShop.Model;

namespace ChopShop.Shop.Web.Models
{
    public class ShoppingBasket
    {
        public List<ShoppingBasketItem> BasketItems { get; set; }
        public Guid CustomerId { get; set; }
    }

    public class ShoppingBasketItem
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}