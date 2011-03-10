using System.Collections.Generic;
using System.Linq;

namespace ChopShop.Admin.Web.Models
{
    public interface IValidation<in T>
    {
        IEnumerable<ErrorInfo> Errors(T validatingService);
        bool IsValid(T validatingService);
    }

    public abstract class AdminValidation<T> : IValidation<T> where T : class
    {
        public virtual IEnumerable<ErrorInfo> Errors(T validatingService)
        {
            return null;
        }

        public bool IsValid(T validatingService)
        {
            return !Errors(validatingService).Any();
        }
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
