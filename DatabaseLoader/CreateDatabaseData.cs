using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using ChopShop.Model;
using ChopShop.NHibernate;
using NHibernate;
using NUnit.Framework;

namespace DatabaseLoader
{
    [TestFixture]
    public class CreateDatabaseData
    {

        private ISession session;

        [SetUp]
        public void Setup()
        {
            session = SessionManager.SessionFactory.OpenSession();
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            // Clear down all the data in the database
            var types = typeof(Entity).Assembly.GetTypes().Where(
                x => x.BaseType == typeof(Entity) && !x.IsAbstract).ToArray();


            RunSqlNonQuery("exec sp_MSForEachTable 'Alter table ? nocheck constraint all '");
            foreach (var type in types)
            {
                session.Delete(string.Format("from {0} o", type.Name));
            }
            session.Flush();

            RunSqlNonQuery("exec sp_MSForEachTable 'alter table ? check constraint all'");
        }

        private void RunSqlNonQuery(string sql)
        {
            var cmd = session.Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = 120;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            session.Flush();
        }

        [TearDown]
        public void TearDown()
        {
            if (session != null)
            {
                session.Flush();
                session.Close();
                session.Dispose();
            }
        }

        [Test]
        [Ignore("Use only when overriding database")]
        public void ThisMethod_should_create_data_for_ChopShop_when_it_is_executed()
        {
            using (var tx = session.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                // Create Categories
                var engines = new Category { Name = "Engines", Description = "All the engines you can imagine" };
                var tyres = new Category { Name = "Tyres", Description = "We have round tyres and square tyres!" };
                var parts = new Category { Name = "Body Parts", Description = "Doors, Wings and all sorts of Panels" };
                var cars = new Category { Name = "Cars", Description = "All shapes, most sizes, lots of prices" };
                var lorries = new Category { Name = "Lorries", Description = "Red ones, Yellow ones and even Blue ones" };
                var bicycles = new Category
                                   {
                                       Name = "Bicycles",
                                       Description = "Some of these have two wheels - some have only one"
                                   };
                var bicycleParts = new Category { Name = "Bicycle Parts", Description = "Derailleurs, Gears, Pedals" };

                engines.Parent = cars;
                tyres.Parent = cars;
                bicycleParts.Parent = bicycles;
                parts.Parent = lorries;

                var categories = new List<Category>
                                     {
                                         engines,
                                         tyres,
                                         parts,
                                         cars,
                                         lorries,
                                         bicycles,
                                         bicycleParts
                                     };
                foreach (var category in categories)
                {
                    session.Save(category);
                }

                var price = new Price();
                price.Value = 10;
                price.Currency = Currency.GBP;

                // Create Products
                var bluepedal = new Product
                                    {
                                        Name = "Blue Pedal",
                                        Description = "This pedal is blue",
                                        Quantity = 10,
                                        Sku = "BluePedal0001",
                                        Prices = new List<Price> {price}
                                    };
                var redpedal = new Product
                                   {
                                       Name = "Red Pedal",
                                       Description = "This pedal is red",
                                       Quantity = 3,
                                       Sku = "RedPedal0002",
                                       Prices = new List<Price> { price }
                                   };
                var bigEngine = new Product
                                    {
                                        Name = "3.0 L V12",
                                        Description = "This is a fast one",
                                        Sku = "Eng00v12",
                                        Quantity = 2,
                                        Prices = new List<Price> { price }
                                    };

                var products = new List<Product> { bluepedal, redpedal, bigEngine };
                products.AddRange(GetProducts());
                foreach (var product in products)
                {
                    session.Save(product);
                }

                // Associate Categories To Products
                bicycleParts.Products = new List<Product> { bluepedal, redpedal };
                engines.Products = new List<Product> { bigEngine };

                session.SaveOrUpdate(bicycleParts);
                session.SaveOrUpdate(engines);

            
                var adminUser = new AdminUser { Email = "admin@example.com", Name = "admin", Password = "password" };
                session.SaveOrUpdate(adminUser);
                

                tx.Commit();
            }
        }

        private List<Product> GetProducts()
        {
            var products = new List<Product>();
            for (int i = 1; i <= 50; i++)
            {
                var price = new Price{Value = 10 + i, Currency = Currency.GBP};
                products.Add(new Product { Name = string.Format("Product {0}", i), Description = string.Format("Description for Product {0}", i), Quantity = i + 10, Sku = string.Format("0000{0}", i), Prices = new List<Price> { price } });
            }
            return products;
        }
    }
}
