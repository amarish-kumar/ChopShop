using System;
using System.Collections.Generic;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Model;
using NHibernate.Criterion;

namespace ChopShop.Admin.Services.Repositories
{
    public class AdminUserRepository : RepositoryBase, IRepository<AdminUser>
    {
        public void Add(AdminUser entity)
        {
            throw new NotImplementedException();
        }

        public void Update(AdminUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(AdminUser entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdminUser> Search(DetachedCriteria searchParameters)
        {
            throw new NotImplementedException();
        }

        public int Count(DetachedCriteria searchParameters)
        {
            throw new NotImplementedException();
        }
    }
}
