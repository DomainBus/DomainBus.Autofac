using System.Reflection;
using Autofac;
using DomainBus;
using DomainBus.Autofac;
using DomainBus.Configuration;
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
        public IntegrationTests()
        {

        }

        [Fact]
        public void everything_is_registered_properly()
        {
            var bus = ServiceBus.ConfigureWith(new AutofacBusRegistration())
                .RegisterInContainer(cb=>cb.RegisterType<MyEvent>().AsSelf())
                .AsMemoryBus(typeof (IntegrationTests).GetTypeInfo().Assembly);


            bus.Container.Resolve<Handler>().Should().NotBeNull();
            bus.Container.Resolve<IDomainBus>().Should().NotBeNull();
            bus.Container.Resolve<IDispatchMessages>().Should().NotBeNull();
            bus.Container.Resolve<MyEvent>().Should().NotBeNull();

        }


    }
}