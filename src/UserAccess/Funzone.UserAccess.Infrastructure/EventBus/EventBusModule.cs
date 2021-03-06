using Autofac;
using Funzone.BuildingBlocks.EventBus.Abstractions;

namespace Funzone.UserAccess.Infrastructure.EventBus
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
            builder.RegisterInstance(_eventBus).SingleInstance();
        }
    }
}