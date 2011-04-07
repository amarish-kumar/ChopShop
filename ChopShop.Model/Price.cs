using System;

namespace ChopShop.Model
{
    public class Price:Entity
    {
        public virtual decimal Value { get; set; }
        public virtual bool IsTaxIncluded { get; set; }
        public virtual decimal TaxRate { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Guid ProductId { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
    }

    public enum Currency
    {
        GBP = 0, USD = 1, EUR = 2
    }
}
