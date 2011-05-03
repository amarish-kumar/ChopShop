using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChopShop.Model
{
    [Serializable]
    public class Basket : Entity
    {
        public virtual List<BasketItem> BasketItems { get; set; }
        public virtual Guid CustomerId { get; set; }

        public void Add(Guid productId, int quantity)
        {
            if (quantity > 0)
            {
                var basketItem = new BasketItem(productId, quantity);
                if (BasketItems == null)
                {
                    BasketItems = new List<BasketItem>();
                }

                BasketItems.Add(basketItem);
            }
        }

        public void Remove(Guid productId)
        {
           BasketItems.RemoveAll(x => x.ProductId == productId);
        }

        public void UpdateQuantity(Guid productId, int quantity)
        {
            Remove(productId);
            Add(productId, quantity);
        }
    }

    [Serializable]
    public class BasketItem : Entity
    {
        public virtual Guid ProductId { get; private set; }
        public virtual int Quantity { get; private set; }

        public BasketItem(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
