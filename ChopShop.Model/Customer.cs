using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChopShop.Model
{
    public class Customer : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual Address BillingAddress { get; set; }
        public virtual Address DeliveryAddress { get; set; }
        public virtual string ContactNumber { get; set; }
    }
}
