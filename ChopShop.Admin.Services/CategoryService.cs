using System;
using System.Collections.Generic;
using System.Linq;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Services.Repositories;
using ChopShop.Admin.Web.Models;
using ChopShop.Model;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Order = NHibernate.Criterion.Order;

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

        public bool TryDelete(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public Category GetSingle(Guid categoryId)
        {
            var searchCriteria = DetachedCriteria.For(typeof (Category))
                .Add(Restrictions.Eq("Id", categoryId));

            return repository.Search(searchCriteria).FirstOrDefault();
        }

        public bool TryAdd(Category category)
        {
            if (IsValid(category))
            {
                repository.Add(category);
                return true;
            }
            return false;
        }

        private bool IsValid(Category category)
        {
            if (CategoryNameExists(category))
            {
                category.AddError(new ErrorInfo("Name", Localisation.ViewModels.EditCategory.CategoryExists));
            }

            return !category.Errors.Any();
        }

        private bool CategoryNameExists(Category category)
        {
            var searchCriteria = DetachedCriteria.For(typeof (Category))
                .Add(!Restrictions.Eq("Id", category.Id))
                .Add(Restrictions.Eq("Name", category.Name));

            var categoriesWithSameNameAndDifferentIds = repository.Count(searchCriteria);
            return categoriesWithSameNameAndDifferentIds > 0;
        }

        public IEnumerable<Category> ListCategoriesForProduct(Guid id)
        {
            var searchCriteria = DetachedCriteria.For(typeof(Category), "c")
                .CreateAlias("Products", "p")
                .Add(Restrictions.Eq("p.Id", id))
                .AddOrder(Order.Asc("Name"))
                .SetResultTransformer(new DistinctRootEntityResultTransformer());


            return repository.Search(searchCriteria);
        }

        public void AddProductToCategory(Guid categoryId, Guid productId)
        {
            var categorySearchCriteria = DetachedCriteria.For(typeof (Category)).SetFetchMode("Products", FetchMode.Join).Add(Restrictions.Eq("Id", categoryId));
            var category = repository.Search(categorySearchCriteria);
            var productSearchCriteria = DetachedCriteria.For(typeof (Product)).Add(Restrictions.Eq("Id", productId));
            var product = ProductRepository.Search(productSearchCriteria);
            category.FirstOrDefault().Products.Add(product.FirstOrDefault());
            repository.Update(category.FirstOrDefault());
        }

        public void RemoveProductFromCategory(Guid categoryId, Guid productId)
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