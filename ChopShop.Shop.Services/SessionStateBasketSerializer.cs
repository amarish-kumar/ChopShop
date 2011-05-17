using System.Web;
using ChopShop.Model;
using ChopShop.Shop.Services.Interfaces;

namespace ChopShop.Shop.Services
{
    public class SessionStateBasketSerializer : IBasketSerializer
    {
        public void Serialize(Basket basket)
        {
            var currentSession = HttpContext.Current.Session;
            currentSession["basket"] = basket;
        }

        public Basket DeSerialize()
        {
            var currentSession = HttpContext.Current.Session;
            if (currentSession["basket"] != null)
            {
                return (Basket)currentSession["basket"];
            }
            return null;
        }

        public void Delete()
        {
            var currentSession = HttpContext.Current.Session;
            currentSession.Remove("basket");
        }
    }
}