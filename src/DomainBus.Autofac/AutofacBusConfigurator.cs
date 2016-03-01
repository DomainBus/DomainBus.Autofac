using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using CavemanTools.Infrastructure;
using DomainBus.Configuration;

namespace DomainBus.Autofac
{
    
 public class AutofacBusConfigurator : AbstractBusBuilder
    {
        private ILifetimeScope _scope;
        private ContainerBuilder _cb=new ContainerBuilder();

        public AutofacBusConfigurator():this(null)
        {
            
        }

        public AutofacBusConfigurator(ILifetimeScope scope)
        {
            _scope = scope;
        }

     public ContainerBuilder ContainerBuilder => _cb;

     protected override void RegisterSingletonInstance<T>(T instance)
        {
            _cb.Register(c => instance).AsSelf().AsImplementedInterfaces().SingleInstance();
        }

     protected override void Register(IEnumerable<Type> types)
     {
            _cb.RegisterTypes(types.ToArray()).AsSelf();
        }

     protected override void RegisterInstanceFactory<T>(Func<T> instance)
     {
         _cb.Register(c => instance()).As<T>().AsSelf();
     }

     protected override void ContainerConfigurationCompleted()
     {
        _cb.Update(_scope.ComponentRegistry);
     }

     protected override IContainerScope BuildContainerScope()
     {
            if (_scope == null)
            {
                _scope = _cb.Build();
            }
            else
            {
                _cb.Update(_scope.ComponentRegistry);
            }
            _cb=new ContainerBuilder();
            return new AutofacWrapper(_scope);
        }

   

     
      
    }
}