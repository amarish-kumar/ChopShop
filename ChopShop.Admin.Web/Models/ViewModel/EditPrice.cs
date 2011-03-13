using System;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Models.ViewModel
{
    public class EditPrice
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public bool IsTaxIncluded { get; set; }
        public decimal TaxRate { get; set; }
        public int Currency { get; set; }
        public Currency Currencies { get; set; }
    }
}