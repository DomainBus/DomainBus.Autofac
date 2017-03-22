using System;
using System.Collections.Generic;
using Autofac;
using CavemanTools.Infrastructure;

namespace DomainBus.Autofac
{
    public class AutofacContainer : IContainerScope
    {
        private readonly ILifetimeScope _container;

        public AutofacContainer(ILifetimeScope container)
        {
            _container = container;
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// If type is not registered return null
        /// </summary>
        /// <param name="type"/>
        /// <returns/>
        public object ResolveOptional(Type type)
        {
            return _container.ResolveOptional(type);
        }

        public T ResolveOptional<T>() where T : class
        {
            return _container.ResolveOptional<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            var rez= _container.Resolve<IEnumerable<T>>();
            if (rez == null)
            {
                return new T[0];
            }
            return rez;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _container.Dispose();
        }

        public IContainerScope BeginLifetimeScope()
        {
            return new AutofacContainer(_container.BeginLifetimeScope());
        }
    }


}