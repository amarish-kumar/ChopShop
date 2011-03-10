using System;
using System.Collections.Generic;
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

        public IEnumerable<Product> List()
        {
            ICollection<Product> products = session.CreateCriteria<Product>()
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

        public IEnumerable<Product> Search(DetachedCriteria searchParameters)
        {
            IEnumerable<Product> products = searchParameters.GetExecutableCriteria(session)
                                                            .List<Product>();

            return products;
        }

        /// <summary>
        /// Loads the product using mapping configuration
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product LoadById(int id)
        {
            return session.Get<Product>(id);
        }

        /// <summary>
        /// Loads the entire product from the database including Categories and Prices
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns></returns>
        public Product LoadObjectGraphById(int id)
        {
            var product = session.CreateCriteria<Product>()
                                 .Add(Restrictions.Eq("Id", id))
                                 .Add(Restrictions.Eq("IsDeleted", false))
                                 .SetFetchMode("Categories", FetchMode.Join)
                                 .SetFetchMode("Prices", FetchMode.Join)
                                 .UniqueResult<Product>();

            return product;
        }

        public int Count(DetachedCriteria searchParameters)
        {
            var count = searchParameters.GetExecutableCriteria(session)
                                        .SetProjection(Projections.RowCountInt64())
                                        .UniqueResult();
            return Convert.ToInt32(count);
        }
    }
}