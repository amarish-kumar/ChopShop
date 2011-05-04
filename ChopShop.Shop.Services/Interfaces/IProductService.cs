using System;
using System.Collections.Generic;
using ChopShop.Model;

namespace ChopShop.Shop.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProductsById(List<Guid> ids);
    }
}
