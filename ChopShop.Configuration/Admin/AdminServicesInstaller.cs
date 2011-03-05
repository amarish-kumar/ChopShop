using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ChopShop.NHibernate;
using NHibernate;

namespace ChopShop.Configuration.Admin
{
    public class AdminServicesInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IWindsorContainer>().Instance(container)); // return itself when a request comes in to resolve the container
            container.Register(AllTypes.FromAssemblyNamed("ChopShop.Admin.Services")
                .Pick()
                .Configure(x=>x.LifeStyle.PerWebRequest)
                .WithService.FirstInterface());
        }
        
    }
}
