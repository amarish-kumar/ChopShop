using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChopShop.Model;
using NUnit.Framework;

namespace ChopShop.Shop.Web.Tests
{
    [TestFixture]
    public class BasketTests
    {
        [Test]
        public void Add_should_add_item_to_item_Collection_when_invoked()
        {
            var productId = Guid.NewGuid();
            var quantity = 10;

            var basket = new Basket();
            basket.Add(productId, quantity);

            Assert.That(basket.BasketItems, Is.Not.Null);
            Assert.That(basket.BasketItems.Count(), Is.EqualTo(1));
            Assert.That(basket.BasketItems[0].ProductId, Is.EqualTo(productId));
            Assert.That(basket.BasketItems[0].Quantity, Is.EqualTo(quantity));
        }

        [Test]
        public void Add_should_not_add_item_to_collection_when_quantity_is_zero()
        {
            var productId = Guid.NewGuid();
            var quantity = 0;

            var basket = new Basket();
            basket.Add(productId, quantity);

            Assert.That(basket.BasketItems, Is.Null);
        }

        [Test]
        public void Add_should_increment_quantity_when_productId_is_already_in_collection()
        {
            var productId = Guid.NewGuid();
            var quantity = 1;

            var basket = new Basket();
            basket.Add(productId, quantity);
            basket.Add(productId, quantity);

            Assert.That(basket.BasketItems, Is.Not.Null);
            Assert.That(basket.BasketItems.Count(), Is.EqualTo(1));
            Assert.That(basket.BasketItems[0].Quantity, Is.EqualTo(2));
        }

        [Test]
        public void Remove_should_remove_item_from_collection_when_invoked()
        {
            var productId = Guid.NewGuid();
            var quantity = 10;

            var basket = new Basket();
            basket.Add(productId, quantity);
            basket.Remove(productId);

            Assert.That(basket.BasketItems.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Remove_should_remove_specific_item_from_collection_when_collection_contains_multiple_items()
        {
            var product1 = Guid.NewGuid();
            var product2 = Guid.NewGuid();
            var quantity1 = 10;
            var quantity2 = 20;

            var basket = new Basket();
            basket.Add(product1, quantity1);
            basket.Add(product2, quantity2);
            basket.Remove(product1);

            Assert.That(basket.BasketItems.Count(), Is.EqualTo(1));
            Assert.That(basket.BasketItems[0].ProductId, Is.EqualTo(product2));
            Assert.That(basket.BasketItems[0].Quantity, Is.EqualTo(quantity2));
        }

        [Test]
        public void UpdateQuantity_should_update_quantity_when_collection_contains_single_item()
        {
            var product1 = Guid.NewGuid();
            var quantity = 10;

            var basket = new Basket();
            basket.Add(product1, quantity);
            quantity = 5;
            basket.UpdateQuantity(product1, quantity);

            Assert.That(basket.BasketItems[0].ProductId, Is.EqualTo(product1));
            Assert.That(basket.BasketItems[0].Quantity, Is.EqualTo(quantity));
        }

        [Test]
        public void UpdateQuantity_should_update_quantity_when_collection_contains_multiple_items()
        {
            var product1 = Guid.NewGuid();
            var quantity1 = 10;
            var product2 = Guid.NewGuid();
            var quantity2 = 20;

            var basket = new Basket();
            basket.Add(product1, quantity1);
            basket.Add(product2, quantity2);
            quantity1 = 5;
            basket.UpdateQuantity(product1, quantity1);

            var itemForProduct1 = basket.BasketItems.FirstOrDefault(x => x.ProductId == product1);

            Assert.That(itemForProduct1, Is.Not.Null);
            Assert.That(itemForProduct1.Quantity, Is.EqualTo(quantity1));
        }
    }
}
