using System.Web.Security;

namespace ChopShop.Admin.Services.Interfaces
{
    public class FormsAuthentication : IFormsAuthentication
    {
        public void SignOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
        }

        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
    }
}