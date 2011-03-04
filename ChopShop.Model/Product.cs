using System;
using System.Collections.Generic;

namespace ChopShop.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<Category> Categories { get; set; }
        public List<Sku> Skus { get; set; }
        public List<CustomField> CustomFields { get; set; }
        public List<Image> Images { get; set; }
    }

    public class Image
    {
        public Guid Id { get; set; }
        public string Reference { get; set; }
    }
}
