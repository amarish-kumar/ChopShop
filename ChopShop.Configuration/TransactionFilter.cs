using System.Data;
using System.Web.Mvc;
using ChopShop.NHibernate;

namespace ChopShop.Configuration
{
    public class TransactionFilter : ActionFilterAttribute
    {
        private readonly TransactionFilterType transactionFilterType;
        public TransactionFilter(TransactionFilterType transactionFilterType)
        {
            this.transactionFilterType = transactionFilterType;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = SessionManager.SessionFactory.GetCurrentSession();
            switch (transactionFilterType)
            {
                case TransactionFilterType.ReadUncommitted:
                    session.BeginTransaction(IsolationLevel.ReadUncommitted);
                    break;
                case TransactionFilterType.ReadCommitted:
                    session.BeginTransaction(IsolationLevel.ReadCommitted);
                    break;
                case TransactionFilterType.Snapshot:
                    session.BeginTransaction(IsolationLevel.Snapshot);
                    break;
                default:
                    session.BeginTransaction(IsolationLevel.ReadUncommitted);
                    break;
            }

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var session = SessionManager.SessionFactory.GetCurrentSession();
            using (session.Transaction)
            {
                if (filterContext.Exception == null)
                {
                    session.Transaction.Commit();
                }
                else
                {
                    session.Transaction.Rollback();
                    session.Close();
                }
            }
        }
    }

    public enum TransactionFilterType
    {
        ReadUncommitted,
        ReadCommitted,
        Snapshot
    }
}
