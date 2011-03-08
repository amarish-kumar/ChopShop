using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ChopShop.Localisation;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Models.ViewModel
{
    public class EditProduct : IValidation
    {
        public int Id { get; set; }
        [LocalisedDisplayName("Name", typeof(Localisation.ViewModels.EditProduct))]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public ICollection<EditPrice> Prices { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<EditCategory> Categories { get; set; }

        public void LoadFromEntity(Product productEntity)
        {
            Id = productEntity.Id;
            Name = productEntity.Name;
            Description = productEntity.Name;
            Sku = productEntity.Sku;
            Prices = productEntity.Prices.Select(x => new EditPrice {Id = x.Id, Value = x.Value, IsTaxIncluded = x.IsTaxIncluded, TaxRate = x.TaxRate}).ToList(); // eew
            IsDeleted = productEntity.IsDeleted;
            Categories =
                productEntity.Categories.Select(
                    x => new EditCategory() {Id = x.Id, Name = x.Name, Description = x.Description}).ToList();
        }

        public IEnumerable<ErrorInfo> Errors()
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ErrorInfo("Name", "Please provide a name for this product");
            }
        }

        public bool IsValid()
        {
            return !Errors().Any();
        }
    }

    public class EditCategory
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }

    public class EditPrice
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public bool IsTaxIncluded { get; set; }
        public decimal TaxRate { get; set; }
    }
}