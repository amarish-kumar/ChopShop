using System.Collections.Generic;

namespace ChopShop.Model
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Code { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Sku> Skus { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
