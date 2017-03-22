using System;
using Autofac;
using DomainBus.Configuration;

namespace DomainBus.Autofac
{
    public static class Extensions
    {
        /// <summary>
        /// Register bus types in container. You still need to use <see cref="AutofacContainer"/> when building bus
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="cfg"></param>
        /// <param name="builder"></param>
        /// <param name="extraConfig">Enable additional features</param>
        /// <returns></returns>
        public static IConfigureHost RegisterWithAutofac(this IConfigureHost bus,ContainerBuilder builder,Action<AutofacBusRegistration> extraConfig=null)
        {
            builder.MustNotBeNull();
            var container = new AutofacBusRegistration(builder);
            extraConfig?.Invoke(container);
            return bus.RegisterTypesInContainer(container);
        }

        
    }
}