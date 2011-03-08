using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChopShop.Admin.Web.Models
{
    public interface IValidation
    {
        IEnumerable<ErrorInfo> Errors();
        bool IsValid();
    }

    public class ErrorInfo
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public ErrorInfo(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
