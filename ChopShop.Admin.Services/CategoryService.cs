using System;
using System.Collections.Generic;
using System.Linq;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Services.Repositories;
using ChopShop.Model;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace ChopShop.Admin.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> repository;

        public IRepository<Product> ProductRepository { get; set; }

        public CategoryService(IRepository<Category> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Category> List()
        {
            var searchCriteria = DetachedCriteria.For(typeof (Category))
                .SetResultTransformer(new DistinctRootEntityResultTransformer());
            return repository.Search(searchCriteria).ToList();
        }

        public bool TryUpdate(Category category)
        {
            throw new NotImplementedException();
        }

        public bool TryDelete(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Category GetSingle(int categoryId)
        {
            var searchCriteria = DetachedCriteria.For(typeof (Category))
                .Add(Restrictions.Eq("Id", categoryId));

            return repository.Search(searchCriteria).FirstOrDefault();
        }

        public bool TryAdd(Category category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> ListCategoriesForProduct(int id)
        {
            var searchCriteria = DetachedCriteria.For(typeof(Category), "c")
                .CreateAlias("Products", "p")
                .Add(Restrictions.Eq("p.Id", id))
                .AddOrder(Order.Asc("Name"))
                .SetResultTransformer(new DistinctRootEntityResultTransformer());


            return repository.Search(searchCriteria);
        }

        public void AddProductToCategory(int categoryId, int productId)
        {
            var categorySearchCriteria = DetachedCriteria.For(typeof (Category)).SetFetchMode("Products", FetchMode.Join).Add(Restrictions.Eq("Id", categoryId));
            var category = repository.Search(categorySearchCriteria);
            var productSearchCriteria = DetachedCriteria.For(typeof (Product)).Add(Restrictions.Eq("Id", productId));
            var product = ProductRepository.Search(productSearchCriteria);
            category.FirstOrDefault().Products.Add(product.FirstOrDefault());
            repository.Update(category.FirstOrDefault());
        }

        public void RemoveProductFromCategory(int categoryId, int productId)
        {
            var categorySearchCriteria = DetachedCriteria.For(typeof(Category)).SetFetchMode("Products", FetchMode.Join).Add(Restrictions.Eq("Id", categoryId));
            var category = repository.Search(categorySearchCriteria);
            var productSearchCriteria = DetachedCriteria.For(typeof(Product)).Add(Restrictions.Eq("Id", productId));
            var product = ProductRepository.Search(productSearchCriteria);
            category.FirstOrDefault().Products.Remove(product.FirstOrDefault());
            repository.Update(category.FirstOrDefault());
        }
    }
}