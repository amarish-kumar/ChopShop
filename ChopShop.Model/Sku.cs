using System;
using System.Collections.Generic;

namespace ChopShop.Model
{
    public class Sku
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Cost Cost { get; set; }
        public List<CustomField> CustomFields { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}