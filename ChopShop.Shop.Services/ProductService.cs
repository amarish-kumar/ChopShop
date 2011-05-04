using System;
using System.Collections.Generic;
using System.Linq;
using ChopShop.Model;
using ChopShop.Shop.Services.Interfaces;
using ChopShop.Shop.Services.Repositories;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace ChopShop.Shop.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> repository;

        public ProductService(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        public List<Product> GetProductsById(List<Guid> ids)
        {
            var searchCriteria = DetachedCriteria.For(typeof (Product))
                .SetFetchMode("Prices", FetchMode.Join)
                .Add(Restrictions.In("Id", ids))
                .SetResultTransformer(new DistinctRootEntityResultTransformer());

            return repository.Search(searchCriteria).ToList();
        }
    }
}