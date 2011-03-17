using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Model;
using NUnit.Framework;

namespace ChopShop.Admin.Web.Tests.ViewModels
{
    [TestFixture]
    public class ProductListItemTests
    {
        [Test]
        public void FromEntityList_should_return_empty_list_when_entity_list_is_null()
        {
            var productList = new ProductListItem().FromEntityList(null);

            Assert.That(productList, Is.Not.Null);
            Assert.That(productList.Any(), Is.False);
        }

        [Test]
        public void FromEntityList_should_return_empty_list_when_entity_list_contains_no_entities()
        {
            var productEntityList = new List<Product>();

            var productList = new ProductListItem().FromEntityList(productEntityList);

            Assert.That(productList, Is.Not.Null);
            Assert.That(productList.Any(), Is.False);
        }

        /// <summary>
        /// Tests like these should become obsolete once we implement automapper
        /// </summary>
        [Test]
        public void FromEntityList_should_map_properties_when_entity_list_contains_entities()
        {
            var productEntityList = Fakes.FakeProductList();

            var productList = new ProductListItem().FromEntityList(productEntityList);

            Assert.That(productList, Is.Not.Null);
            Assert.That(productList.Any(), Is.True);
            Assert.That(productList[0].Name, Is.EqualTo(productEntityList.ToList()[0].Name));
        }
    }
}
