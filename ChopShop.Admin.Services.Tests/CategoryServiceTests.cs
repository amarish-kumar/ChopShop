using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChopShop.Admin.Services.Repositories;
using ChopShop.Admin.Web.Models;
using ChopShop.Model;
using Moq;
using NHibernate.Criterion;
using NUnit.Framework;

namespace ChopShop.Admin.Services.Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<IRepository<Category>> repository;
        private CategoryService service;

        [SetUp]
        public void Setup()
        {
            repository = new Mock<IRepository<Category>>();
            service = new CategoryService(repository.Object);
        }

        [Test]
        public void TryAdd_should_return_false_when_Category_already_exists()
        {
            repository.Setup(x => x.Count(It.IsAny<DetachedCriteria>())).Returns(1).Verifiable();

            var expectedResult = service.TryAdd(new Category());

            Assert.That(expectedResult, Is.False);
            repository.Verify(x=>x.Count(It.IsAny<DetachedCriteria>()), Times.AtLeastOnce());
        }

        [Test]
        public void TryAdd_should_return_true_when_Category_name_does_not_already_exist()
        {
            repository.Setup(x => x.Count(It.IsAny<DetachedCriteria>())).Returns(0).Verifiable();

            var expectedResult = service.TryAdd(new Category());

            Assert.That(expectedResult, Is.True);
            repository.Verify(x=>x.Count(It.IsAny<DetachedCriteria>()), Times.AtLeastOnce());
        }
    }
}
