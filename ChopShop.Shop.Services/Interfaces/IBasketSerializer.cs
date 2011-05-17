using ChopShop.Model;

namespace ChopShop.Shop.Services.Interfaces
{
    public interface IBasketSerializer
    {
        void Serialize(Basket basket);
        Basket DeSerialize();
        void Delete();
    }
}