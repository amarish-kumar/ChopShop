using System;
using System.Linq;
using Castle.Windsor;

namespace ChopShop.Configuration
{
    /// <summary>
    /// http://www.cprieto.com/index.php/2010/07/30/windsor-service-locator-for-asp-net-mvc3-preview-1/
    /// </summary>
    public static class WindsorExtension
    {
        public static void InjectAttribute(this IWindsorContainer container, Type type, object instance)
        {
            var properties = type.GetProperties().Where(x => x.CanWrite && x.PropertyType.IsPublic);
            foreach (var propertyInfo in properties)
            {
                if (container.Kernel.HasComponent(propertyInfo.PropertyType))
                {
                    propertyInfo.SetValue(instance, container.Resolve(propertyInfo.PropertyType), null);
                }
            }
        }
    }
}
