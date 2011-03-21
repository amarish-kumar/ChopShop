using System;
using System.Collections.Generic;
using System.Linq;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Models.ViewModel
{
    public class ProductListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<string> Categories { get; set; }
        public int Quantity { get; set; }
        public string Sku { get; set; }


        public List<ProductListItem> FromEntityList(IEnumerable<Product> productEntityList)
        {
            var productList = new List<ProductListItem>();
            if (productEntityList != null && productEntityList.Any())
            {
                productList.AddRange(productEntityList.Select(x => new ProductListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Prices != null ? (x.Prices.FirstOrDefault() != null ? x.Prices.FirstOrDefault().Value : 0) : 0,
                    Categories = x.Categories != null ? x.Categories.Select(y => y.Name).ToList() : null,
                    Quantity = x.Quantity,
                    Sku = x.Sku
                }).ToList());
            }
            return productList;
        }
    }
}