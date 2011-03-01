using System;
using System.Web.Mvc;
using Castle.MicroKernel;

namespace ChopShop.Configuration
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            kernel.ReleaseComponent(controller);
        }

        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            var controllerComponentName = string.Format("{0}Controller", controllerName);
            try
            {
                return kernel.Resolve<IController>(controllerComponentName);
            }
            catch (ComponentNotFoundException componentNotFoundException)
            {
                throw new ApplicationException(string.Format("No controller with name '{0}' found", controllerName), componentNotFoundException);
            }

        }
    }
}
