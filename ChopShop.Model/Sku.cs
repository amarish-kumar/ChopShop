using System.Collections.Generic;

namespace ChopShop.Model
{
    public class Sku
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual Cost Cost { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int ProductId { get; set; }
    }
}