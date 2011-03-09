using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChopShop.Model;
using ChopShop.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace ChopShop.Admin.Services.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly ISession session;

        public CategoryRepository()
        {
            session = SessionManager.SessionFactory.GetCurrentSession();
        }

        public IEnumerable<Category> List()
        {
            throw new NotImplementedException();
        }

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

        public Category LoadById(int id)
        {
            throw new NotImplementedException();
        }

        public Category LoadObjectGraphById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
