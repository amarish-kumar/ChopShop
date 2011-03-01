using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Windsor;

namespace ChopShop.Configuration
{
    /// <summary>
    /// http://www.cprieto.com/index.php/2010/07/30/windsor-service-locator-for-asp-net-mvc3-preview-1/
    /// </summary>
    public class WindsorFilterAttributeProvider : FilterAttributeFilterProvider
    {
        private readonly IWindsorContainer container;

        public WindsorFilterAttributeProvider(IWindsorContainer container)
        {
            this.container = container;
        }

        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetControllerAttributes(controllerContext, actionDescriptor);
            foreach (var attribute in attributes)
            {
                container.InjectAttribute(attribute.GetType(), attribute);
            }

            return attributes;
        }

        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetActionAttributes(controllerContext, actionDescriptor);
            foreach (var attribute in attributes)
            {
                container.InjectAttribute(attribute.GetType(), attribute);
            }
            return attributes;
        }
    }
}
