using System.Web;
using ChopShop.Model;
using ChopShop.Shop.Services.Interfaces;

namespace ChopShop.Shop.Services
{
    public class SessionBasketService : IBasketService
    {
        public void Save(Basket basket)
        {
            var currentSession = HttpContext.Current.Session;
            currentSession["basket"] = basket;
        }

        public void Clear()
        {
            var currentSession = HttpContext.Current.Session;
            currentSession.Remove("basket");
        }

        public Basket GetCurrentBasket()
        {
            var currentSession = HttpContext.Current.Session;
            if (currentSession["basket"] != null)
            {
                return (Basket) currentSession["basket"];
            }
            return null;
        }
    }
}