using ChopShop.NHibernate;
using NHibernate;

namespace ChopShop.Shop.Services.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly ISession session;

        protected RepositoryBase()
        {
            session = SessionManager.SessionFactory.GetCurrentSession();
        }
    }
}