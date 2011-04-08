using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Controllers;
using ChopShop.Admin.Web.Models;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Model;
using Moq;
using NUnit.Framework;

namespace ChopShop.Admin.Web.Tests.Controllers
{
    [TestFixture]
    public class CategoryControllerTests
    {
        private Mock<ICategoryService> service;
        private CategoryController controller;

        [SetUp]
        public void Setup()
        {
            service = new Mock<ICategoryService>();
            controller = new CategoryController(service.Object);
        }

        [Test]
        public void CategoriesForSelectDialog_should_return_JsonResult_when_invoked()
        {
            service.Setup(x => x.List()).Returns(Fakes.FakeCategoryList()).Verifiable();
            service.Setup(x => x.ListCategoriesForProduct(It.IsAny<Guid>())).Returns(Fakes.FakeCategoryList())
                .Verifiable();

            var action = controller._CategoriesForSelectDialog(Guid.NewGuid());

            Assert.That(action, Is.Not.Null);
            Assert.That(action, Is.InstanceOf<JsonResult>());
            service.VerifyAll();
        }

        [Test]
        public void CategoriesForProduct_should_return_JsonResult_when_invoked()
        {
            service.Setup(x => x.ListCategoriesForProduct(It.IsAny<Guid>())).Returns(Fakes.FakeCategoryList()).
                Verifiable();

            var action = controller._CategoriesForProduct(Guid.NewGuid());

            Assert.That(action, Is.Not.Null);
            Assert.That(action, Is.InstanceOf<JsonResult>());
            service.Verify(x => x.ListCategoriesForProduct(It.IsAny<Guid>()), Times.AtLeastOnce());
        }

        [Test]
        public void Add_should_not_invoke_service_when_ModelState_is_Invalid()
        {
            service.Setup(x => x.TryAdd(It.IsAny<Category>())).Verifiable();

            controller.ModelState.AddModelError("fake error", "fake error");
            var action = controller.Add(new EditCategory());

            Assert.That(action, Is.Not.Null);
            service.Verify(x => x.TryAdd(It.IsAny<Category>()), Times.Never());
        }

        [Test]
        public void _Add_should_not_invoke_service_when_ModelState_is_Invalid()
        {
            service.Setup(x => x.TryAdd(It.IsAny<Category>())).Verifiable();

            controller.ModelState.AddModelError("fake error", "fake error");
            var action = controller._Add(new EditCategory());

            Assert.That(action, Is.Not.Null);
            Assert.That(action, Is.InstanceOf<JsonResult>());
            service.Verify(x => x.TryAdd(It.IsAny<Category>()), Times.Never());
        }

        [Test]
        public void Add_should_have_invalid_ModelState_when_Service_returns_false()
        {
            service.Setup(x => x.TryAdd(It.IsAny<Category>())).Callback<Category>(x=>x.AddError(new ErrorInfo("fake error", "Fake Error"))).Returns(false).Verifiable();

            var action = controller.Add(new EditCategory());

            Assert.That(action, Is.Not.Null);
            Assert.That(controller.ModelState.IsValid, Is.False);
        }

        [Test]
        public void _Add_should_have_invalid_ModelState_when_Service_returns_false()
        {
            service.Setup(x => x.TryAdd(It.IsAny<Category>())).Callback<Category>(x => x.AddError(new ErrorInfo("fake error", "Fake Error"))).Returns(false).Verifiable();

            var action = controller._Add(new EditCategory());

            Assert.That(action, Is.Not.Null);
            Assert.That(controller.ModelState.IsValid, Is.False);
        }

        [Test]
        public void Add_should_redirect_to_Edit_page_when_category_is_added_successfully()
        {
            service.Setup(x => x.TryAdd(It.IsAny<Category>())).Returns(true).Verifiable();

            var action = controller.Add(new EditCategory()) as RedirectToRouteResult;

            Assert.That(action, Is.Not.Null);
            Assert.That(action.RouteValues["Action"], Is.EqualTo("Edit"));
            service.Verify(x=>x.TryAdd(It.IsAny<Category>()), Times.AtLeastOnce());
        }

        [Test]
        public void _Add_should_return_CategoryId_as_JsonResult_when_category_is_added_successfully()
        {
            var fakeCategory = new Category {Id = Guid.NewGuid()};
            service.Setup(x => x.TryAdd(It.IsAny<Category>())).Callback<Category>(x => x.Id = fakeCategory.Id).Returns(
                true).Verifiable();

            var action = controller._Add(new EditCategory());

            Assert.That(action, Is.Not.Null);
            Assert.That(action, Is.InstanceOf<JsonResult>());
            Assert.That(action.Data, Is.EqualTo(fakeCategory.Id));
        }

        [Test]
        public void Edit_should_contain_ModelStateErrors_when_Product_is_not_found()
        {
            service.Setup(x => x.GetSingle(It.IsAny<Guid>())).Returns((Category)null).Verifiable();

            var action = controller.Edit(Guid.NewGuid());
            var model = (EditCategory)action.ViewData.Model;

            Assert.That(action, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(controller.ModelState.IsValid, Is.False);
            service.Verify(x => x.GetSingle(It.IsAny<Guid>()), Times.AtMostOnce());
        }

    }
}
