using System.Configuration;
using System.Reflection;
using NHibernate;
using Configuration = NHibernate.Cfg.Configuration;
using NHibernate.Tool.hbm2ddl;

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
                var configuration = new Configuration()
                    .AddAssembly(Assembly.GetExecutingAssembly())
                    .SetProperty("connection.connection_string", GetCoreConnectionString())
                    .Configure();

//#if (DEBUG)
//                {
                //var schemadrop = new SchemaExport(configuration);
                //schemadrop.Drop(true,false);
                
                //var schemaUpdate = new SchemaUpdate(configuration);
                
                //schemaUpdate.Execute(true, true);

                //var schemaCreate = new SchemaExport(configuration);
                //schemaCreate.Create(true, true);
//                }
//#endif

                sessionFactory = configuration.BuildSessionFactory();
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
