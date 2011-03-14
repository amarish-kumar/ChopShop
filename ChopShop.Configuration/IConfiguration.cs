using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChopShop.Configuration
{
    public interface IConfiguration
    {
        Model.Configuration GetConfiguration();
    }

    public class Configuration : IConfiguration
    {
        public Model.Configuration GetConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}
