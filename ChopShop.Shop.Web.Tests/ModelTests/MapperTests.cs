using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using ChopShop.Shop.Web.Configuration;
using NUnit.Framework;

namespace ChopShop.Shop.Web.Tests.ModelTests
{
    [TestFixture]
    public class MapperTests
    {
        [Test]
        public void AutoMapper_should_map_correctly()
        {
            var mapper = new ModelMapper();
            mapper.Map();

            Mapper.AssertConfigurationIsValid();
        }
    }
}
