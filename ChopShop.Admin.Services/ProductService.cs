using System;
using System.Collections.Generic;
using System.Linq;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Services.Repositories;
using ChopShop.Admin.Web.Models;
using ChopShop.Model;
using ChopShop.Model.DTO;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace ChopShop.Admin.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> repository;
        private readonly IRepository<Price> priceRepository;

        public ProductService(IRepository<Product> repository, IRepository<Price> priceRepository)
        {
            this.repository = repository;
            this.priceRepository = priceRepository;
        }

        public IEnumerable<Product> List()
        {
            var searchCriteria = DetachedCriteria.For(typeof(Product))
                                                 .SetFetchMode("Categories", FetchMode.Join)
                                                 .SetFetchMode("Prices", FetchMode.Join)
                                                 .SetResultTransformer(new DistinctRootEntityResultTransformer());
            return repository.Search(searchCriteria).ToList();
        }

        /// <summary>
        /// Try to update the Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>False if Product does not meet business rules.  Errors are contained in the Errors Collection of the Product</returns>
        public bool TryUpdate(Product product)
        {
            if (IsValid(product))
            {
                repository.Update(product);
                return true;
            }
            return false;
        }

        public bool TryDelete(Guid productId)
        {
            var searchCriteria = DetachedCriteria.For(typeof(Product))
                                                 .Add(Restrictions.Eq("Id", productId));
            var product = repository.Search(searchCriteria).FirstOrDefault();
            product.IsDeleted = true;
            repository.Update(product);
            return true;
        }

        public Product GetSingle(Guid productId)
        {
            var searchCriteria = DetachedCriteria.For(typeof(Product))
                                                 .Add(Restrictions.Eq("Id", productId))
                                                 .SetFetchMode("Categories", FetchMode.Join)
                                                 .SetFetchMode("Prices", FetchMode.Join)
                                                 .SetResultTransformer(new DistinctRootEntityResultTransformer());

            var product = repository.Search(searchCriteria);

            return product.FirstOrDefault();
        }

        /// <summary>
        /// Try to add a new Product to the database
        /// </summary>
        /// <param name="product"></param>
        /// <returns>False if Product does not meet business rules.  Errors are contained in the Errors collection of the Product</returns>
        public bool TryAdd(Product product)
        {
            if (IsValid(product))
            {
                repository.Add(product);
                return true;
            }

            return false;
        }

        public bool TryAddPrice(Price price)
        {
            priceRepository.Add(price);
            return true;
        }

        public IEnumerable<Product> List(ProductListSearchCriteria listSearchCriteria)
        {
            var searchCriteria = GetSearchCriteriaFromListSearch(listSearchCriteria);
            return repository.Search(searchCriteria);
        }

        private DetachedCriteria GetSearchCriteriaFromListSearch(ProductListSearchCriteria listSearchCriteria)
        {

            var searchCriteria = DetachedCriteria.For(typeof(Product), "p")
                                                 .SetFetchMode("Prices", FetchMode.Join)
                                                 .SetFetchMode("Categories", FetchMode.Join)
                                                 .SetResultTransformer(new DistinctRootEntityResultTransformer());

            // add ordering by Property
            var sortBy = listSearchCriteria.SortBy ?? "Name";
            searchCriteria.AddOrder(listSearchCriteria.Ascending
                                        ? Order.Asc(sortBy)
                                        : Order.Desc(sortBy));

            if (!listSearchCriteria.ShowDeletedProducts)
            {
                searchCriteria.Add(Restrictions.Eq("IsDeleted", false)); // default to showing not deleted products
            }


            return searchCriteria;
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

            var productsWithSameSkuAndDifferentIds = repository.Count(searchCriteria);
            return productsWithSameSkuAndDifferentIds > 0;
        }
    }
}