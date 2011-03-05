using System;
using System.Collections.Generic;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Services.Repositories;
using ChopShop.Model;

namespace ChopShop.Admin.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public ICollection<Product> List()
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
    }
}