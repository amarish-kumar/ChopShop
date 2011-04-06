using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ChopShop.Model;
using Moq;

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

        public static IEnumerable<Category> FakeCategoryList()
        {
            var categories = new List<Category>();
            for (int i = 0; i < 10; i++)
            {
                categories.Add(new Category{Name="Category " + i, Description="Category Description " + i});
            }
            return categories;
        }

        public static Mock<ControllerContext> MockedContext()
        {
            var mockControllerContext = new Mock<ControllerContext>();
            var mockHttpContextBase = new Mock<HttpContextBase>();
            var mockHttpSessionStateBase = new Mock<HttpSessionStateBase>();
            mockHttpContextBase.Setup(x => x.Session).Returns(mockHttpSessionStateBase.Object);
            mockControllerContext.Setup(x => x.HttpContext).Returns(mockHttpContextBase.Object);
           return mockControllerContext;
        }
    }
}
