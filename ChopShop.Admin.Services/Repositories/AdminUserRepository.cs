using System;
using System.Collections.Generic;
using ChopShop.Model;
using ChopShop.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace ChopShop.Admin.Services.Repositories
{
    public class AdminUserRepository : IRepository<AdminUser>
    {
        private readonly ISession session;

        public AdminUserRepository()
        {
            session = SessionManager.SessionFactory.GetCurrentSession();
        }

        public IEnumerable<AdminUser> List()
        {
            throw new NotImplementedException();
        }

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

        public AdminUser LoadById(int id)
        {
            return session.Get<AdminUser>(id);
        }

        public AdminUser LoadObjectGraphById(int id)
        {
            throw new NotImplementedException();
        }

        public int Count(DetachedCriteria searchParameters)
        {
            throw new NotImplementedException();
        }
    }
}
