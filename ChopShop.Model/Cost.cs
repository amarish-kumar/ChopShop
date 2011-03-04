using System;

namespace ChopShop.Model
{
    public class Cost
    {
        public virtual int Id { get; set; }
        public virtual Decimal Value { get; set; }
        public virtual bool IsTaxIncluded { get; set; }
        public virtual decimal TaxRate { get; set; }
        public virtual TaxType TaxType { get; set; }
    }
}
