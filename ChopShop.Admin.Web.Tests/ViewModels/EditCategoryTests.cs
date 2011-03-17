using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Model;
using NUnit.Framework;

namespace ChopShop.Admin.Web.Tests.ViewModels
{
    [TestFixture]
    public class EditCategoryTests
    {
        [Test]
        public void Name_should_have_Required_attribute()
        {
            var propertyInfo = typeof (EditCategory).GetProperty("Name");
            var attribute = propertyInfo.GetCustomAttributes(typeof (RequiredAttribute), false)
                .Cast<RequiredAttribute>()
                .FirstOrDefault();

            Assert.That(attribute, Is.Not.Null);
        }

        [Test]
        public void Name_should_have_max_length_255_characters()
        {
            var propertyInfo = typeof (EditCategory).GetProperty("Name");
            var attribute = propertyInfo.GetCustomAttributes(typeof (StringLengthAttribute), false)
                .Cast<StringLengthAttribute>()
                .FirstOrDefault();

            Assert.That(attribute.MaximumLength, Is.EqualTo(255));
        }

        [Test]
        public void FromEntity_should_map_properties()
        {
            var categoryEntity = new Category {Name = "Category 1", Description = "Category 1 Description"};
            var category = new EditCategory();
            
            category.FromEntity(categoryEntity);

            Assert.That(category.Name, Is.EqualTo(categoryEntity.Name));
            Assert.That(category.Description, Is.EqualTo(categoryEntity.Description));
        }

        [Test]
        public void ToEntity_should_map_properties()
        {
            var category = new EditCategory {Description = "Category 2 Description", Name = "Category 2 Name"};

            var categoryEntity = category.ToEntity();

            Assert.That(categoryEntity.Name, Is.EqualTo(category.Name));
            Assert.That(categoryEntity.Description, Is.EqualTo(category.Description));
        }
    }
}
