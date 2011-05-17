using ChopShop.Model;
using ChopShop.Shop.Services.Interfaces;

namespace ChopShop.Shop.Services
{
    public class BasketService : IBasketService
    {
        private IBasketSerializer serializer;

        public BasketService()
        {
            GetSerializationOption();
        }
        
        private void GetSerializationOption()
        {
            //TODO: Get the serialization method from the database
            serializer = new SessionStateBasketSerializer();
        }

        public void Save(Basket basket)
        {
            serializer.Serialize(basket);
        }

        public void Clear()
        {
            serializer.Delete();
        }

        public Basket GetCurrentBasket()
        {
            return serializer.DeSerialize();
        }
    }
}