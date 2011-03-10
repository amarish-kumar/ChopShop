using System.Collections.Generic;
using NHibernate.Criterion;

namespace ChopShop.Admin.Services.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> List();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> Search(DetachedCriteria searchParameters); // NHibernate leak is acceptable due to upfront dependency on NHib (i.e. by design)
        T LoadById(int id);
        T LoadObjectGraphById(int id);
        int Count(DetachedCriteria searchParameters);
    }
}
