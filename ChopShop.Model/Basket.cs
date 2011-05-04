using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChopShop.Model
{
    public class Basket : Entity
    {
        public virtual List<BasketItem> BasketItems { get; set; }
        public virtual Guid CustomerId { get; set; }

        public void Add(Guid productId, int quantity)
        {
            if (quantity <= 0) return;
            var basketItem = new BasketItem(productId, quantity);
            if (BasketItems == null)
            {
                BasketItems = new List<BasketItem> {basketItem};
                return;
            }

            if (BasketItems.Any(x => x.ProductId == productId))
            {
                var item = BasketItems.FirstOrDefault(x => x.ProductId == productId);
                var newQuantity = item.Quantity + quantity;
                UpdateQuantity(productId, newQuantity);
            }
            else
            {
                BasketItems.Add(basketItem); 
            }
        }

        public void Remove(Guid productId)
        {
            if (BasketItems == null) return;
            BasketItems.RemoveAll(x => x.ProductId == productId);
        }

        public void UpdateQuantity(Guid productId, int quantity)
        {
            if (BasketItems == null) return;
            Remove(productId);
            Add(productId, quantity);
        }
    }

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
