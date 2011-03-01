using System.Configuration;
using System.Reflection;
using NHibernate;
using Configuration = NHibernate.Cfg.Configuration;

namespace ChopShop.NHibernate
{
    public sealed class SessionManager
    {
        private readonly ISessionFactory sessionFactory;
        public static ISessionFactory SessionFactory { get { return Instance.sessionFactory; } }
        public static SessionManager Instance { get { return NestedSessionManager.sessionManager; } }

        private ISessionFactory GetSessionFactory()
        {
            return sessionFactory;
        }

        public static ISession OpenSession()
        {
            return Instance.GetSessionFactory().OpenSession(new UtcDateTimeInterceptor());
        }

        private SessionManager()
        {
            if (sessionFactory == null)
            {
                sessionFactory = new Configuration()
                    .AddAssembly(Assembly.GetExecutingAssembly())
                    .SetProperty("connection.connection_string", GetCoreConnectionString())
                    .Configure()
                    .BuildSessionFactory();
            }
        }

        private static string GetCoreConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ChopShop"].ToString();
        }

        class NestedSessionManager
        {
            internal static readonly SessionManager sessionManager = new SessionManager();
        }
    }


}
