using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ChopShop.Shop.Services;
using ChopShop.Shop.Services.Interfaces;

namespace ChopShop.Configuration.Shop
{
    public class ShopServicesInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IWindsorContainer>().Instance(container)); // return itself when a request comes in to resolve the container

            // hard code the session basket for the moment
            container.Register(Component.For<IBasketService>().ImplementedBy(typeof(SessionBasketService)));

            container.Register(AllTypes.FromAssemblyNamed("ChopShop.Shop.Services")
                                   .Pick()
                                   .Configure(x => x.LifeStyle.PerWebRequest)
                                   .WithService.FirstInterface());
        }
    }
}