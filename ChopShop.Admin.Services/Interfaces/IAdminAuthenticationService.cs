using System.Web;
using ChopShop.Model;

namespace ChopShop.Admin.Services.Interfaces
{
    public interface IAdminAuthenticationService
    {
        void SignIn(AdminUser adminUser, HttpSessionStateBase session);
        void SignOut(HttpSessionStateBase httpSessionStateBase);
        bool IsValidUser(string email, string password, AdminUser adminUser);
    }
}
