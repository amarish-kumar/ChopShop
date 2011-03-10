using System.Collections.Generic;
using System.Linq;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Models.DTO;
using ChopShop.Model;
using NHibernate;
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
            var searchCriteria = DetachedCriteria.For(typeof (Product))
                                                 .SetFetchMode("Categories", FetchMode.Join)
                                                 .SetFetchMode("Prices", FetchMode.Join);
            return productRepository.Search(searchCriteria);
        }

        public void Add(Product product)
        {
            productRepository.Add(product);
        }

        public void Update(Product product)
        {
            productRepository.Update(product);
        }

        public void Delete(int productId)
        {
            var searchCriteria = DetachedCriteria.For(typeof (Product))
                                                 .Add(Restrictions.Eq("Id", productId));
            var product = productRepository.Search(searchCriteria).FirstOrDefault();
            product.IsDeleted = true;
            productRepository.Update(product);
        }

        public Product GetSingle(int productId)
        {
            var searchCriteria = DetachedCriteria.For(typeof (Product))
                                                 .Add(Restrictions.Eq("Id", productId))
                                                 .SetFetchMode("Categories", FetchMode.Join)
                                                 .SetFetchMode("Prices", FetchMode.Join);

            var product = productRepository.Search(searchCriteria);

            return product.FirstOrDefault();
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