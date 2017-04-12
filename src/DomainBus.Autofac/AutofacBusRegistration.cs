using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using CavemanTools.Infrastructure;
using DomainBus.Configuration;
using DomainBus.DomainEvents;

namespace DomainBus.Autofac
{
    
 public class AutofacBusRegistration : IRegisterBusTypesInContainer
    {
      private ContainerBuilder _cb=new ContainerBuilder();

        public AutofacBusRegistration(ContainerBuilder cb)
        {
            _cb = cb;
        }

        public AutofacBusRegistration WithCommandResultSupport()
        {
            _cb.RegisterType<CommandResultMediator>().AsImplementedInterfaces().SingleInstance();
            _cb.Register(c => c.Resolve<IDomainBus>().GetCommandResultMediator(c.Resolve<ICommandResultMediator>()))
                .AsImplementedInterfaces().SingleInstance();
            return this;
        }

        /// <summary>
        /// Requires implementation of <see cref="IAppendEventsToStore"/> to be registered in the container
        /// </summary>
        /// <param name="singleton"></param>
        /// <returns></returns>
        public AutofacBusRegistration WithEventStorePublisherSupport(bool singleton=true)
        {
            var reg =
                _cb.Register(
                        c => c.Resolve<IDomainBus>().CreateEventsStoreAndPublisher(c.Resolve<IAppendEventsToStore>()))
                    .As<IStoreAndPublishEvents>();
            if (singleton) reg.SingleInstance();
            return this;
        }

         public ContainerBuilder ContainerBuilder => _cb;

        public void RegisterSingletonInstance<T>(T instance)
        {
            _cb.Register(c => instance).AsSelf().AsImplementedInterfaces().SingleInstance();
        }

        public void Register(Type[] types, bool asSingleton=false)
     {
         if (!asSingleton)
         {
             _cb.RegisterTypes(types).AsSelf();
                return;
         }
            _cb.RegisterTypes(types).AsSelf().SingleInstance();
        }

        public void RegisterInstanceFactory<T>(Func<T> instance)
     {
         _cb.Register(c => instance()).As<T>().AsSelf();
     }
     

    
       
    }
}