using System.Collections.Generic;

namespace ChopShop.Model
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int ProductId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}