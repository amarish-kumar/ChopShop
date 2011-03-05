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
    public class EditProduct
    {
        public int Id { get; set; }
        [LocalisedDisplayName("Name", typeof(Localisation.ViewModels.EditProduct))]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public EditCost Cost { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Category> Categories { get; set; }
    }

    public class EditCost
    {
        public int Id { get; set; }
        public Decimal Value { get; set; }
        public bool IsTaxIncluded { get; set; }
        public decimal TaxRate { get; set; }
        public EditTaxType TaxType { get; set; }
    }

    public class EditTaxType
    {
        public string TaxType { get; set; }
    }
}