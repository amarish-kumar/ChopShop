using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ChopShop.Shop.Web.Controllers;

namespace ChopShop.Admin.Web.Configuration
{
    public class ControllersInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromThisAssembly()
                .BasedOn<IController>()
                .If(Component.IsInSameNamespaceAs<HomeController>())
                .If(t=>t.Name.EndsWith("Controller"))
                .Configure((ConfigureDelegate)(c=>c.Named(c.ServiceType.Name).LifeStyle.Transient))
                );
        }
    }
}
