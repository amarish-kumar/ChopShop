using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChopShop.Model
{
    public class AdminUser : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
    }
}
