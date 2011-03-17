using System;
using System.Web;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Model;

namespace ChopShop.Admin.Services
{
    public class AdminAuthenticationService : IAdminAuthenticationService
    {
        private readonly IAdminService adminService;
        private readonly IFormsAuthenticationService formsAuthenticationService;
        private AdminUser adminUser;

        public AdminAuthenticationService(IAdminService adminService, IFormsAuthenticationService formsAuthenticationService)
        {
            this.adminService = adminService;
            this.formsAuthenticationService = formsAuthenticationService;
        }

        public void SignIn(string userName, HttpSessionStateBase session)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("Cannot authenticate without a valid email address");
            }

            formsAuthenticationService.SetAuthCookie(userName, false);
            session.Add("adminUser", adminUser);
        }

        public void SignOut(HttpSessionStateBase session)
        {
            formsAuthenticationService.SignOut();
            session.Abandon();
        }

        public bool IsValidUser(string email, string password)
        {
            adminUser = adminService.GetUserForLogin(email, password);
            return adminUser != null;
        }
    }
}