using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ChopShop.Admin.Web.Models.ViewModel;
using NUnit.Framework;


namespace ChopShop.Admin.Web.Tests.ViewModels
{
    [TestFixture]
    /// see http://bradwilson.typepad.com/blog/2009/04/dataannotations-and-aspnet-mvc.html
    public class EditProductTests
    {
        [Test]
        public void Name_should_have_Required_attribute()
        {
            var propertyInfo = typeof (EditProduct).GetProperty("Name");
            var attribute = propertyInfo.GetCustomAttributes(typeof (RequiredAttribute), false)
                .Cast<RequiredAttribute>()
                .FirstOrDefault();

            Assert.That(attribute, Is.Not.Null);
        }

        [Test]
        public void Name_should_have_max_length_255_characters()
        {
            var propertyInfo = typeof (EditProduct).GetProperty("Name");
            var attribute = propertyInfo.GetCustomAttributes(typeof (StringLengthAttribute), false)
                .Cast<StringLengthAttribute>()
                .FirstOrDefault();

            Assert.That(attribute.MaximumLength, Is.EqualTo(255));
        }

        [Test]
        public void Sku_should_have_Required_attribute()
        {
            var propertyInfo = typeof(EditProduct).GetProperty("Sku");
            var attribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false)
                .Cast<RequiredAttribute>()
                .FirstOrDefault();

            Assert.That(attribute, Is.Not.Null);
        }

        [Test]
        public void Sku_should_have_max_length_100_characters()
        {
            var propertyInfo = typeof(EditProduct).GetProperty("Sku");
            var attribute = propertyInfo.GetCustomAttributes(typeof(StringLengthAttribute), false)
                .Cast<StringLengthAttribute>()
                .FirstOrDefault();

            Assert.That(attribute.MaximumLength, Is.EqualTo(100));
        }

        [Test]
        public void FromEntity_should_map_properties_when_invoked()
        {
            var fakeProduct = Fakes.FakeProduct();
            var editProduct = new EditProduct();
            
            editProduct.FromEntity(fakeProduct);

            Assert.That(editProduct.Name, Is.EqualTo(fakeProduct.Name));
            Assert.That(editProduct.Description, Is.EqualTo(fakeProduct.Description));
            Assert.That(editProduct.Sku, Is.EqualTo(fakeProduct.Sku));
        }

        [Test]
        public void ToEntity_should_map_properties_when_invoked()
        {
            var editProduct = new EditProduct {Name = "Product Name", Description = "Product Description", Sku = "123"};

            var product = editProduct.ToEntity();

            Assert.That(product.Name, Is.EqualTo(editProduct.Name));
            Assert.That(product.Description, Is.EqualTo(editProduct.Description));
            Assert.That(product.Sku, Is.EqualTo(editProduct.Sku));
        }
    }
    
}
