using Autofac;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.Services.Albums.Application.IntegrationEvents.EventHandling;

namespace Funzone.Services.Albums.Infrastructure.EventBus
{
    public class EventBusModule : Autofac.Module
    {
        private readonly IEventBus _eventBus;

        public EventBusModule(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(_eventBus)
                .SingleInstance();

            builder
                .RegisterType<UserRegisteredIntegrationEventHandler>()
                .AsSelf();
        }
    }
}