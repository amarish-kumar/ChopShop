using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChopShop.Model;

namespace ChopShop.Shop.Services.Interfaces
{
    public interface IBasketService
    {
        void Save(Basket basket);
        void Clear();
        Basket GetCurrentBasket();
    }
}
