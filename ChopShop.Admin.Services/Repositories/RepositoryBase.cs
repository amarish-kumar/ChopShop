using ChopShop.NHibernate;
using NHibernate;

namespace ChopShop.Admin.Services.Repositories
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