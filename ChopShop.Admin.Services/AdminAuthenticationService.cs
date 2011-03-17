using System;
using System.Web;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Model;

namespace ChopShop.Admin.Services
{
    public class AdminAuthenticationService : IAdminAuthenticationService
    {
        private readonly IAdminService adminService;
        private readonly IFormsAuthentication formsAuthentication;

        public AdminAuthenticationService(IAdminService adminService, IFormsAuthentication formsAuthentication)
        {
            this.adminService = adminService;
            this.formsAuthentication = formsAuthentication;
        }

        public void SignIn(AdminUser adminUser, HttpSessionStateBase session)
        {
            if (adminUser == null)
            {
                throw new ArgumentNullException("Cannot authenticate without a valid email address");
            }

            formsAuthentication.SetAuthCookie(adminUser.Name, false);
            session.Add("adminUser", adminUser);
        }

        public void SignOut(HttpSessionStateBase session)
        {
            formsAuthentication.SignOut();
            session.Abandon();
        }

        public bool IsValidUser(string email, string password, AdminUser adminUser)
        {
            adminUser = adminService.GetUserForLogin(email, password);
            return adminUser != null;
        }
    }
}