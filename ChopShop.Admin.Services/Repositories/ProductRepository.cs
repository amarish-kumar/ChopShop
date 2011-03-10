using System;
using System.Collections.Generic;
using ChopShop.Admin.Services.Interfaces;
using ChopShop.Model;
using NHibernate.Criterion;

namespace ChopShop.Admin.Services.Repositories
{
    public class ProductRepository : RepositoryBase, IRepository<Product>
    {
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
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Search(DetachedCriteria searchParameters)
        {
            IEnumerable<Product> products = searchParameters.GetExecutableCriteria(session)
                                                            .List<Product>();

            return products;
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