namespace ChopShop.Admin.Services.Interfaces
{
    public interface IFormsAuthentication
    {
        void SignOut();
        void SetAuthCookie(string userName, bool createPersistentCookie);
    }
}
