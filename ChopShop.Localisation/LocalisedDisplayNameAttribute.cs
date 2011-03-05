using System;
using System.ComponentModel;
using System.Reflection;

namespace ChopShop.Localisation
{
    public class LocalisedDisplayNameAttribute : DisplayNameAttribute
    {
        private PropertyInfo propertyInfo;

        public LocalisedDisplayNameAttribute(string resourceKey, Type localisedResource) : base(resourceKey)
        {
            propertyInfo = localisedResource.GetProperty(base.DisplayName, BindingFlags.Static | BindingFlags.Public);
        }

        public override string DisplayName
        {
            get
            {
                if (propertyInfo == null)
                {
                    return base.DisplayName;
                }
                return (string) propertyInfo.GetValue(propertyInfo.DeclaringType, null);
            }
        }
    }
}
