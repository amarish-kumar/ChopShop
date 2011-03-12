using System;
using System.Collections.Generic;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Services.Repositories;
using ChopShop.Model;
using NHibernate.Criterion;

namespace ChopShop.Admin.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> repository;

        public CategoryService(IRepository<Category> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Category> List()
        {
            var searchCriteria = DetachedCriteria.For(typeof (Category));
            return repository.Search(searchCriteria);
        }

        public bool TryUpdate(Category product)
        {
            throw new NotImplementedException();
        }

        public bool TryDelete(int productId)
        {
            throw new NotImplementedException();
        }

        public Category GetSingle(int productId)
        {
            throw new NotImplementedException();
        }

        public bool TryAdd(Category product)
        {
            throw new NotImplementedException();
        }
    }
}