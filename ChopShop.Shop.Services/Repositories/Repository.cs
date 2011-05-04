using System.Collections.Generic;
using NHibernate.Criterion;

namespace ChopShop.Shop.Services.Repositories
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T: class
    {
        public void Add(T entity)
        {
            session.Save(entity);
        }

        public void Update(T entity)
        {
            session.Update(entity);
        }

        public void Delete(T entity)
        {
            session.Delete(entity);
        }

        public IEnumerable<T> Search(DetachedCriteria searchParameters)
        {
            IEnumerable<T> entities = searchParameters.GetExecutableCriteria(session).Future<T>();

            return entities;
        }

        public int Count(DetachedCriteria searchParameters)
        {
            var count = searchParameters.GetExecutableCriteria(session)
                                        .SetProjection(Projections.RowCount())
                                        .FutureValue<int>();
            return count.Value;
        }
    }
}