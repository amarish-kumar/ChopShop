using AutoMapper;
using ChopShop.Model;
using ChopShop.Shop.Web.Models;

namespace ChopShop.Shop.Web.Configuration
{
    public class ModelMapper
    {
        public void Map()
        {
            Mapper.CreateMap<BasketItem, ShoppingBasketItem>()
                  .ForMember(x=>x.Name, opt=>opt.Ignore())
                  .ForMember(x=>x.Price, opt=>opt.Ignore())
                  .ForMember(x=>x.Currency, opt=>opt.Ignore());
            Mapper.CreateMap<Basket, ShoppingBasket>();
        }
    }
}