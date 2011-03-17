using System.Web.Security;

namespace ChopShop.Admin.Services.Interfaces
{
    public interface IFormsAuthenticationService
    {
        void SignOut();
        void SetAuthCookie(string userName, bool createPersistentCookie);
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
    }
}
