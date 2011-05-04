using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ChopShop.Model;
using ChopShop.Shop.Web.Models;

namespace ChopShop.Shop.Web.Configuration
{
    public class ModelMapper
    {
        public void Map()
        {
            Mapper.CreateMap<BasketItem, ShoppingBasketItem>();
            Mapper.CreateMap<Basket, ShoppingBasket>();
        }
    }
}