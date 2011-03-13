using System;
using System.Collections.Generic;
using ChopShop.Model;

namespace ChopShop.Admin.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> List();
        bool TryUpdate(Category product);
        bool TryDelete(Guid productId);
        Category GetSingle(Guid productId);
        bool TryAdd(Category product);
        IEnumerable<Category> ListCategoriesForProduct(Guid id);
        void AddProductToCategory(Guid categoryId, Guid productId);
        void RemoveProductFromCategory(Guid categoryId, Guid productId);
    }
}
