using System;
using System.Collections.Generic;
using System.Linq;
using ChopShop.Localisation;
using ChopShop.Model;

namespace ChopShop.Admin.Web.Models.ViewModel
{
    public class ProductListItem
    {
        public Guid Id { get; set; }
        [LocalisedDisplayName("Name", typeof(Localisation.ViewModels.EditProduct))]
        public string Name { get; set; }
        [LocalisedDisplayName("Price", typeof(Localisation.ViewModels.EditProduct))]
        public IEnumerable<Price> Prices { get; set; }
        [LocalisedDisplayName("Categories", typeof(Localisation.ViewModels.EditProduct))]
        public List<string> Categories { get; set; }
        [LocalisedDisplayName("Quantity", typeof(Localisation.ViewModels.EditProduct))]
        public int Quantity { get; set; }
        [LocalisedDisplayName("Sku", typeof(Localisation.ViewModels.EditProduct))]
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
                    Prices = x.Prices,
                    Categories = x.Categories != null ? x.Categories.Select(y => y.Name).ToList() : null,
                    Quantity = x.Quantity,
                    Sku = x.Sku
                }).ToList());
            }
            return productList;
        }
    }
}