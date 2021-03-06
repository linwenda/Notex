using Autofac;
using Funzone.BuildingBlocks.EventBus.MassTransit;
using Funzone.BuildingBlocks.Infrastructure.EventBus;

namespace Funzone.UserAccess.Infrastructure.EventBus
{
    public class EventBusModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MassTransitEventBus>()
                .As<IEventBus>()
                .SingleInstance();
        }
    }
}