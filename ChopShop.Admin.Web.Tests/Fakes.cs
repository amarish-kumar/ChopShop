using System;
using System.Collections.Generic;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Tests
{
    public static class Fakes
    {
        public static Product FakeProduct()
        {
            return new Product { Id = Guid.NewGuid(), Name = "Product 1", Sku = "Product 1", Description = "Product Description" };
        }

        public static IEnumerable<Product> FakeProductList()
        {
            var products = new List<Product>();
            for (int i = 0; i < 10; i++)
            {
                products.Add(new Product { Name = string.Format("Product{0}", i), Sku = string.Format("Product {0}", i), Description = string.Format("ProductDescription{0}", i) });
            }
            return products;
        }
    }
}
