using System;
using System.Collections.Generic;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Model;
using NHibernate.Criterion;

namespace ChopShop.Admin.Services.Repositories
{
    public class CategoryRepository : RepositoryBase, IRepository<Category>
    {
        public void Add(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> Search(DetachedCriteria searchParameters)
        {
            throw new NotImplementedException();
        }

        public int Count(DetachedCriteria searchParameters)
        {
            throw new NotImplementedException();
        }
    }
}
