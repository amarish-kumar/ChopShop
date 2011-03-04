using System;

namespace ChopShop.Model
{
    public class Cost
    {
        public Guid Id { get; set; }
        public Decimal Value { get; set; }
        public Currency Currency { get; set; }
        public bool IsTaxIncluded { get; set; }
        public decimal TaxRate { get; set; }
        public TaxType TaxType { get; set; }
    }
}
