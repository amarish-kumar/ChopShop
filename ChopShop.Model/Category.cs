using System.Collections.Generic;

namespace ChopShop.Model
{
    public class Category :Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual Category Parent { get; set; }
    }
}