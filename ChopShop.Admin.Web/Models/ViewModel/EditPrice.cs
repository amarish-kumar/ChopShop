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
        public Currency Currency { get; set; }
        public Guid ProductId { get; set; }

        public Price ToEntity()
        {
            var price = new Price
                            {
                                Value = this.Value,
                                IsTaxIncluded = this.IsTaxIncluded,
                                TaxRate = this.TaxRate,
                                Currency = this.Currency,
                                ProductId = this.ProductId
                            };

            return price;
        }
    }
}