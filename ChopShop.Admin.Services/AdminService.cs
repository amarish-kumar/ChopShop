using System;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Admin.Services.Repositories;
using ChopShop.Model;
using NHibernate.Criterion;

namespace ChopShop.Admin.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<AdminUser> adminUserRepository;

        public AdminService(IRepository<AdminUser> adminUserRepository)
        {
            this.adminUserRepository = adminUserRepository;
        }

        public AdminUser GetUserForLogin(string email, string password)
        {
            var searchCriteria = DetachedCriteria.For(typeof (AdminUser), "adminUser")
                                                 .Add(Restrictions.Eq("Email", email))
                                                 .Add(Restrictions.Eq("Password", password));


            throw new NotImplementedException();
        }
    }
}       