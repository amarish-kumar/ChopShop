using System;
using System.Linq;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Services.Repositories;
using ChopShop.Model;
using NHibernate.Criterion;

namespace ChopShop.Admin.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<AdminUser> repository;

        public AdminService(IRepository<AdminUser> repository)
        {
            this.repository = repository;
        }

        public AdminUser GetUserForLogin(string email, string password)
        {
            var searchCriteria = DetachedCriteria.For(typeof (AdminUser), "adminUser")
                                                 .Add(Restrictions.Eq("Email", email))
                                                 .Add(Restrictions.Eq("Password", password));


            var adminUser = repository.Search(searchCriteria).FirstOrDefault();
            if (adminUser != null)
            {
                adminUser.LastLogin = DateTime.UtcNow;
                repository.Update(adminUser);
            }

            return adminUser;
        }
    }
}       