using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChopShop.Localisation
{
    public interface ILocalisationService
    {
        string LocalisedValue(string key);
    }

    public class LocalisationService : ILocalisationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string LocalisedValue(string key)
        {
            return "foo";
        }
    }
}
