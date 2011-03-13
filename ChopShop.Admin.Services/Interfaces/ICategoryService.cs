using System.Collections.Generic;
using ChopShop.Model;

namespace ChopShop.Admin.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> List();
        bool TryUpdate(Category product);
        bool TryDelete(int productId);
        Category GetSingle(int productId);
        bool TryAdd(Category product);
        IEnumerable<Category> ListCategoriesForProduct(int id);
        void AddProductToCategory(int categoryId, int productId);
        void RemoveProductFromCategory(int categoryId, int productId);
    }
}
