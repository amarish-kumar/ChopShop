using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Controllers;
using ChopShop.Admin.Web.Models;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Model;
using ChopShop.Model.DTO;
using Moq;
using NUnit.Framework;

namespace ChopShop.Admin.Web.Tests.Controllers
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductService> service;
        private ProductController controller;

        [SetUp]
        public void Setup()
        {
            service = new Mock<IProductService>();
            controller = new ProductController(service.Object);
        }

        [Test]
        public void List_should_invoke_List_on_ProductService()
        {
            service.Setup(x => x.List(It.IsAny<ProductListSearchCriteria>())).Returns(FakeProductList()).Verifiable();

            var action = controller.List(new ProductListSearchCriteria());
            var model = (List<ProductListItem>)action.ViewData.Model;

            Assert.That(action, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model[0].Name, Is.EqualTo("Product0"));
            Assert.That(model.Count, Is.EqualTo(10));
            service.Verify(x=>x.List(It.IsAny<ProductListSearchCriteria>()), Times.AtMostOnce());
        }

        [Test]
        public void Edit_should_get_product_from_ProductService_when_invoked()
        {
            service.Setup(x => x.GetSingle(It.IsAny<Guid>())).Returns(FakeProduct()).Verifiable();

            var action = controller.Edit(Guid.NewGuid());
            var model = (EditProduct) action.ViewData.Model;

            Assert.That(action, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Name, Is.EqualTo("Product 1"));
            Assert.That(controller.ModelState.IsValid, Is.True);
            service.Verify(x=>x.GetSingle(It.IsAny<Guid>()), Times.AtMostOnce());
        }

        [Test]
        public void Edit_should_contain_ModelStateErrors_when_Product_is_not_found()
        {
            service.Setup(x => x.GetSingle(It.IsAny<Guid>())).Returns((Product) null).Verifiable();

            var action = controller.Edit(Guid.NewGuid());
            var model = (EditProduct) action.ViewData.Model;

            Assert.That(action, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(controller.ModelState.IsValid, Is.False);
            service.Verify(x=>x.GetSingle(It.IsAny<Guid>()), Times.AtMostOnce());
        }

        [Test]
        public void Add_should_return_Edit_view_when_invoked()
        {
            var action = controller.Add();

            Assert.That(action, Is.Not.Null);
            Assert.That(action.ViewName, Is.EqualTo("Edit"));
        }

        [Test]
        public void Add_should_not_invoke_service_when_ModelState_is_Invalid()
        {
            service.Setup(x => x.TryAdd(It.IsAny<Product>())).Verifiable();

            controller.ModelState.AddModelError("fake error", "fake error");
            var action = controller.Add(new EditProduct());

            Assert.That(action, Is.Not.Null);
            service.Verify(x=>x.TryAdd(It.IsAny<Product>()), Times.Never());
        }

        [Test]
        public void Add_should_redirect_to_Edit_page_when_product_is_added_successfully()
        {
            service.Setup(x => x.TryAdd(It.IsAny<Product>())).Returns(true).Verifiable();

            var action = controller.Add(new EditProduct()) as RedirectToRouteResult;

            Assert.That(action, Is.Not.Null);
            Assert.That(action.RouteValues["Action"], Is.EqualTo("Edit"));
            service.Verify(x=>x.TryAdd(It.IsAny<Product>()), Times.AtMostOnce());
        }

        [Test]
        public void Add_should_have_invalid_Modelstate_when_Service_returns_false()
        {
            service.Setup(x => x.TryAdd(It.IsAny<Product>())).Callback<Product>(x => x.AddError(new ErrorInfo("fake error", "fake error"))).Returns(false).Verifiable();
            var action = controller.Add(new EditProduct());

            Assert.That(action, Is.Not.Null);
            Assert.That(controller.ModelState.IsValid, Is.False);
        }

        private Product FakeProduct()
        {
            return new Product
                       {Id = Guid.NewGuid(), Name = "Product 1", Sku = "Product 1", Description = "Product Description"};
        }

        private IEnumerable<Product> FakeProductList()
        {
            var products = new List<Product>();
            for (int i = 0; i < 10; i++)
            {
                products.Add(new Product
                                 {Name = string.Format("Product{0}", i), Sku = string.Format("Product {0}", i), Description = string.Format("ProductDescription{0}", i)});
            }
            return products;
        }
    }
}
