using System.Collections.Generic;
using NHibernate.Criterion;

namespace ChopShop.Admin.Services.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> Search(DetachedCriteria searchParameters); // NHibernate leak is acceptable due to upfront dependency on NHib (i.e. by design)
        int Count(DetachedCriteria searchParameters);
    }
}
