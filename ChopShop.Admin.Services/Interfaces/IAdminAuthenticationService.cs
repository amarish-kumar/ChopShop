using System.Web;

namespace ChopShop.Admin.Services.Interfaces
{
    public interface IAdminAuthenticationService
    {
        void SignIn(string userName, HttpSessionStateBase session);
        void SignOut(HttpSessionStateBase httpSessionStateBase);
        bool IsValidUser(string email, string password);
    }
}
