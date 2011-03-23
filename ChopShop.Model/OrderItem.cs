using System;

namespace ChopShop.Model
{
    public class OrderItem : Entity
    {
        public virtual Guid ProductId { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Price Price { get; set; }
        public virtual Guid OrderId { get; set; }
    }
}