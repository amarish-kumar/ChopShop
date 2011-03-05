using System;
using System.Collections.Generic;
using System.Linq;
using ChopShop.Model;
using ChopShop.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace ChopShop.Admin.Services.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ISession session;

        public ProductRepository()
        {
            session = SessionManager.SessionFactory.GetCurrentSession();
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

        public void Add(Product entity)
        {
            using (var transaction = session.BeginTransaction(System.Data.IsolationLevel.Snapshot))
            {
                session.Save(entity);
                transaction.Commit();
            }
        }

        public void Update(Product product)
        {
            using (var transaction = session.BeginTransaction(System.Data.IsolationLevel.Snapshot))
            {
                session.Update(product);
                transaction.Commit();
            }
        }

        public void Delete(Product product)
        {
            product.IsDeleted = true;
            Update(product);
        }

        public ICollection<Product> Search(DetachedCriteria searchParameters)
        {
            ICollection<Product> products;
            using (var transaction = session.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                products = searchParameters.GetExecutableCriteria(session).List<Product>();
                transaction.Commit();
            }
            return products;
        }

        public Product LoadById(int id)
        {
            Product product;
            using (var transaction = session.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                product = session.Get<Product>(id);
                transaction.Commit();
            }
            return product;
        }
    }
}