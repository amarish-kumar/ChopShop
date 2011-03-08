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
                products = session.CreateCriteria<Product>()
                                  .List<Product>();
            return products;
        }

        public void Add(Product entity)
        {
            session.Save(entity);
        }

        public void Update(Product product)
        {
            session.Update(product);
        }

        public void Delete(Product product)
        {
            product.IsDeleted = true;
            Update(product);
        }

        public ICollection<Product> Search(DetachedCriteria searchParameters)
        {
            ICollection<Product> products = searchParameters.GetExecutableCriteria(session).List<Product>();

            return products;
        }

        public Product LoadById(int id)
        {
            return session.Get<Product>(id);
        }
    }
}