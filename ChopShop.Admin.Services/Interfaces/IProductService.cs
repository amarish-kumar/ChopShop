using System;
using System.Collections.Generic;
using ChopShop.Model;
using ChopShop.NHibernate;
using NHibernate;

namespace ChopShop.Admin.Services.Interfaces
{
    public interface IProductService
    {
        ICollection<Product> List();
    }

    public class ProductService : IProductService
    {
        private readonly ISession session;

        public ProductService()
        {
            if (session == null)
            {
                session = SessionManager.SessionFactory.GetCurrentSession();
            }
        }

        public ICollection<Product> List()
        {
            ICollection<Product> products;
            using (var transaction = session.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                products = session.CreateCriteria<Product>()
                            .List<Product>();
                transaction.Commit();
            }
            return products;
        }
    }
}
