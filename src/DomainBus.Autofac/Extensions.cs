using System;
using Autofac;
using DomainBus.Configuration;

namespace DomainBus.Autofac
{
    public static class Extensions
    {
        /// <summary>
        /// Register additional types in container
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static IBuildBusWithContainer RegisterInContainer(this IBuildBusWithContainer bus,Action<ContainerBuilder> cfg)
        {
            var autofac = bus as AutofacBusConfigurator;
            cfg?.Invoke(autofac.ContainerBuilder);
            return bus;
        }
    }
}