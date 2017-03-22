using System.Reflection;
using System.Runtime.InteropServices;
using Autofac;
using DomainBus;
using DomainBus.Autofac;
using DomainBus.Configuration;
using DomainBus.DomainEvents;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class MyEvent : AbstractEvent
    {
        
    }

    public class Handler
    {
        public void Handle(MyEvent ev)
        {
            
        }
    }

    public class IntegrationTests
    {
        ContainerBuilder _cb=new ContainerBuilder();
        private AutofacBusRegistration _sut;

        public IntegrationTests()
        {            
            _sut =new AutofacBusRegistration(_cb);
        }

        [Fact]
        public void everything_is_registered_properly()
        {
            var c=    ServiceBus.ConfigureAsMemoryBus(_sut, GetType().GetTypeInfo().Assembly);
            var container = _cb.Build();
            c.Build(new AutofacContainer(container));
            
            container.Resolve<Handler>().Should().NotBeNull();
            container.Resolve<IDomainBus>().Should().NotBeNull();
            container.Resolve<IDispatchMessages>().Should().NotBeNull();
            

        }

        class FakeappendStore:IAppendEventsToStore
        {
            public void Append(params DomainEvent[] events)
            {
                throw new System.NotImplementedException();
            }
        }

        [Fact]
        public void extra_features_added_properly()
        {
            _cb.RegisterType<FakeappendStore>().As<IAppendEventsToStore>();
            var c = ServiceBus.ConfigureForMonolith(
                hc =>
                {
                    hc.RegisterWithAutofac(_cb, a => a.WithCommandResultSupport().WithEventStorePublisherSupport())
                        .RegisterHandlersAndSagaStatesFrom(GetType().GetTypeInfo().Assembly)
                        .ConfigureProcessors(p => p.Add(ServiceBus.MemoryProcessor, s => s.HandlesEverything()));
                }
                );
            var container = _cb.Build();
            c.Build(new AutofacContainer(container));
            container.Resolve<ICommandResultMediator>().Should().NotBeNull();
            container.Resolve<IRequestCommandResult>().Should().NotBeNull();
            container.Resolve<IReturnCommandResult>().Should().NotBeNull();
            container.Resolve<IStoreAndPublishEvents>().Should().NotBeNull();

        }

    }
}