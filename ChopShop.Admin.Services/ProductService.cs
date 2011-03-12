using System.Collections.Generic;
using System.Linq;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Web.Models;
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

        public bool TryUpdate(Product product)
        {
            if (IsValid(product))
            {
                productRepository.Update(product);
                return true;    
            }
            return false;
        }

        public bool TryDelete(int productId)
        {
            var searchCriteria = DetachedCriteria.For(typeof (Product))
                                                 .Add(Restrictions.Eq("Id", productId));
            var product = productRepository.Search(searchCriteria).FirstOrDefault();
            product.IsDeleted = true;
            productRepository.Update(product);
            return true;
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

        public bool TryAdd(Product product)
        {
            if (IsValid(product))
            {
                productRepository.Add(product);
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Ensure the product is validated against business rules
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private bool IsValid(Product product)
        {
            if (SkuExists(product))
            {
                product.AddError(new ErrorInfo("Sku", Localisation.ViewModels.EditProduct.SkuExists));
            }

            return !product.Errors.Any();
        }

        /// <summary>
        /// Business Rule: Sku's must be unique
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private bool SkuExists(Product product)
        {
            var searchCriteria = DetachedCriteria.For(typeof(Product))
                                                 .Add(!Restrictions.Eq("Id", product.Id))
                                                 .Add(Restrictions.Eq("Sku", product.Sku));

            var productsWithSameSkuAndDifferentIds = productRepository.Count(searchCriteria);
            return productsWithSameSkuAndDifferentIds > 0;
        }
    }
}