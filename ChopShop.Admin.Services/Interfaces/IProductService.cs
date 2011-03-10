using System.Collections.Generic;
using ChopShop.Admin.Web.Models.DTO;
using ChopShop.Model;

namespace ChopShop.Admin.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> List();
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
        Product GetSingle(int productId);
        bool SkuExists(SearchProduct product);
    }
}
