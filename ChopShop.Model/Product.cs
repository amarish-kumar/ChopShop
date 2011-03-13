using System.Collections.Generic;
using ChopShop.Admin.Web.Models;

namespace ChopShop.Model
{
    public class Product : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Sku { get; set; }
        public virtual ICollection<Price> Prices { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual int Quantity { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Image> Images { get; set; }

        private List<ErrorInfo> errors;
        public virtual List<ErrorInfo> Errors
        {
            get
            {
                return errors ?? new List<ErrorInfo>();
            }
        }

        public virtual void AddError(ErrorInfo errorInfo)
        {
            if (errors == null)
            {
                errors = new List<ErrorInfo>();
            }

            errors.Add(errorInfo);
        }

    }
}
