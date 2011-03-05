using System;

namespace ChopShop.Model
{
    public class Cost
    {
        public virtual int Id { get; set; }
        public virtual decimal Value { get; set; }
        public virtual bool IsTaxIncluded { get; set; }
        public virtual decimal TaxRate { get; set; }
        public virtual int ProductId { get; set; }
        public virtual Currency Currency { get; set; }
    }

    public enum Currency
    {
        GBP, USD, EUR
    }
}
