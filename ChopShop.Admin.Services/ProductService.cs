using System;
using System.Collections.Generic;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Services.Repositories;
using ChopShop.Admin.Web.Models.DTO;
using ChopShop.Model;
using NHibernate.Criterion;

namespace ChopShop.Admin.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public IEnumerable<Product> List()
        {
            return productRepository.List();
        }

        public void Add(Product product)
        {
           // perform any business logic here
            productRepository.Add(product);
        }

        public void Update(Product product)
        {
            // perform any business logic here
            productRepository.Update(product);
        }

        public void Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public Product GetSingle(int productId)
        {
            return productRepository.LoadObjectGraphById(productId);
        }

        public bool SkuExists(SearchProduct searchProduct)
        {
            var searchCriteria = DetachedCriteria.For(typeof (Product))
                                                 .Add(!Restrictions.Eq("Id", searchProduct.Id))
                                                 .Add(Restrictions.Eq("Sku", searchProduct.Sku));

            var productsWithSameSkuAndDifferentIds = productRepository.Count(searchCriteria);
            return productsWithSameSkuAndDifferentIds > 0;
        }
    }
}