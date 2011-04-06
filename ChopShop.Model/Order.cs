using System.Collections.Generic;

namespace ChopShop.Model
{
    public class Order : Entity
    {
        public virtual Customer Customer { get; set; }
        public virtual Address BillingAddress { get; set; }
        public virtual Address DeliveryAddress { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
