namespace ChopShop.Admin.Web.Models
{
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