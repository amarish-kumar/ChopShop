using System.Collections.Generic;

namespace ChopShop.Model
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Sku { get; set; }
        public virtual ICollection<Price> Prices { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual int Quantity { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
