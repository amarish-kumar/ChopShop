using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Controllers;
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

            var action = controller.CategoriesForSelectDialog(Guid.NewGuid());

            Assert.That(action, Is.Not.Null);
            Assert.That(action, Is.InstanceOf<JsonResult>());
            service.VerifyAll();
        }

        [Test]
        public void CategoriesForProduct_should_return_JsonResult_when_invoked()
        {
            service.Setup(x => x.ListCategoriesForProduct(It.IsAny<Guid>())).Returns(Fakes.FakeCategoryList()).
                Verifiable();

            var action = controller.CategoriesForProduct(Guid.NewGuid());

            Assert.That(action, Is.Not.Null);
            Assert.That(action, Is.InstanceOf<JsonResult>());
            service.Verify(x => x.ListCategoriesForProduct(It.IsAny<Guid>()), Times.AtLeastOnce());
        }
    }
}
