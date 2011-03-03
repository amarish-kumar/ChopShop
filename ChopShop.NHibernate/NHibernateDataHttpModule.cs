using System;
using System.Web;
using NHibernate;
using NHibernate.Context;

namespace ChopShop.NHibernate
{
    public class NHibernateDataHttpModule : IHttpModule
    {
        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application </param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
            context.EndRequest += context_EndRequest;
        }

        private void context_EndRequest(object sender, EventArgs e)
        {
            ISession session = ManagedWebSessionContext.Unbind(HttpContext.Current, SessionManager.SessionFactory);
            if (session == null) return;
            if (session.Transaction != null && session.Transaction.IsActive)
            {
                session.Transaction.Rollback();
            }
            else
            {
                session.Flush();
            }
            session.Close();
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            ManagedWebSessionContext.Bind(HttpContext.Current, SessionManager.SessionFactory.OpenSession());
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
