using System;
using System.Collections.Generic;
using ChopShop.Model;

namespace ChopShop.Admin.Services.Interfaces
{
    public interface IProductService
    {
        ICollection<Product> List();
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
        Product GetSingle(int productId);
    }
}
