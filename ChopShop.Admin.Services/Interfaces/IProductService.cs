using System.Collections.Generic;
using ChopShop.Model;

namespace ChopShop.Admin.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> List();
        bool TryUpdate(Product product);
        bool TryDelete(int productId);
        Product GetSingle(int productId);
        bool TryAdd(Product product);
    }
}
