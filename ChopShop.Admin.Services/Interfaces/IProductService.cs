using System;
using System.Collections.Generic;
using ChopShop.Model;
using ChopShop.Model.DTO;

namespace ChopShop.Admin.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> List();
        bool TryUpdate(Product product);
        bool TryDelete(Guid productId);
        Product GetSingle(Guid productId);
        bool TryAdd(Product product);
        bool TryAddPrice(Price price);
        Tuple<IEnumerable<Product>, int> List(ProductListSearchCriteria searchCriteria);
    }
}
