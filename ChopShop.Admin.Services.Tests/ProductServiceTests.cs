using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChopShop.Admin.Services.Repositories;
using ChopShop.Model;
using Moq;
using NHibernate.Criterion;
using NUnit.Framework;

namespace ChopShop.Admin.Services.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IRepository<Product>> repository;
        private Mock<IRepository<Price>> priceRepository;
        private ProductService service;

        [SetUp]
        public void Setup()
        {
            repository = new Mock<IRepository<Product>>();
            priceRepository = new Mock<IRepository<Price>>();

            service = new ProductService(repository.Object, priceRepository.Object);
        }

        [Test]
        public void TryUpdate_should_return_false_and_not_update_when_Sku_exists()
        {
            repository.Setup(x => x.Count(It.IsAny<DetachedCriteria>())).Returns(1).Verifiable();
            repository.Setup(x => x.Update(It.IsAny<Product>())).Verifiable();

            var result = service.TryUpdate(new Product());

            Assert.That(result, Is.False);
            repository.Verify(x => x.Count(It.IsAny<DetachedCriteria>()), Times.AtLeastOnce());
            repository.Verify(x => x.Update(It.IsAny<Product>()), Times.Never());
        }

        [Test]
        public void TryUpdate_should_add_to_Product_Error_collection_when_Sku_exist()
        {
            repository.Setup(x => x.Count(It.IsAny<DetachedCriteria>())).Returns(1).Verifiable();
            repository.Setup(x => x.Update(It.IsAny<Product>())).Verifiable();

            var product = new Product();

            var result = service.TryUpdate(product);

            Assert.That(result, Is.False);
            Assert.That(product.Errors.Any(), Is.True);
            repository.Verify(x => x.Count(It.IsAny<DetachedCriteria>()), Times.AtLeastOnce());
            repository.Verify(x => x.Update(It.IsAny<Product>()), Times.Never());
        }

        [Test]
        public void TryUpdate_should_invoke_repository_update_when_Sku_does_not_exist()
        {
            repository.Setup(x => x.Count(It.IsAny<DetachedCriteria>())).Returns(0).Verifiable();
            repository.Setup(x => x.Update(It.IsAny<Product>())).Verifiable();

            var product = new Product();
            var result = service.TryUpdate(product);

            Assert.That(result, Is.True);
            Assert.That(product.Errors, Is.Empty);
            repository.Verify(x => x.Count(It.IsAny<DetachedCriteria>()), Times.AtLeastOnce());
            repository.Verify(x => x.Update(It.IsAny<Product>()), Times.AtLeastOnce());
        }

        [Test]
        public void TryDelete_should_set_IsDeleted_property_to_true_and_update_when_invoked()
        {
            var products = new List<Product> { new Product { IsDeleted = false } };
            repository.Setup(x => x.Update(It.IsAny<Product>())).Verifiable();
            repository.Setup(x => x.Search(It.IsAny<DetachedCriteria>())).Returns(products).Verifiable();

            var result = service.TryDelete(Guid.NewGuid());

            Assert.That(result, Is.True);
            Assert.That(products[0].IsDeleted, Is.True);
            repository.Verify(x => x.Search(It.IsAny<DetachedCriteria>()), Times.AtLeastOnce());
            repository.Verify(x => x.Update(It.IsAny<Product>()), Times.AtLeastOnce());
        }

        [Test]
        public void TryAdd_should_return_false_and_not_add_when_Sku_exists()
        {
            repository.Setup(x => x.Count(It.IsAny<DetachedCriteria>())).Returns(1).Verifiable();
            repository.Setup(x => x.Add(It.IsAny<Product>())).Verifiable();

            var result = service.TryAdd(new Product());

            Assert.That(result, Is.False);
            repository.Verify(x => x.Count(It.IsAny<DetachedCriteria>()), Times.AtLeastOnce());
            repository.Verify(x => x.Add(It.IsAny<Product>()), Times.Never());
        }

        [Test]
        public void TryAdd_should_populate_Product_error_collection_when_Sku_exists()
        {
            repository.Setup(x => x.Count(It.IsAny<DetachedCriteria>())).Returns(1);
            repository.Setup(x => x.Add(It.IsAny<Product>())).Verifiable();
            var product = new Product();

            var result = service.TryAdd(product);

            Assert.That(result, Is.False);
            Assert.That(product.Errors.Any(), Is.True);
            repository.Verify(x=>x.Add(It.IsAny<Product>()), Times.Never());
        }

        [Test]
        public void TryAdd_should_add_product_when_Sku_does_not_already_exist()
        {
            repository.Setup(x => x.Count(It.IsAny<DetachedCriteria>())).Returns(0);
            repository.Setup(x => x.Add(It.IsAny<Product>())).Verifiable();

            var result = service.TryAdd(new Product());

            Assert.That(result, Is.True);
            repository.Verify(x=>x.Add(It.IsAny<Product>()), Times.AtLeastOnce());
        }
    }
}
