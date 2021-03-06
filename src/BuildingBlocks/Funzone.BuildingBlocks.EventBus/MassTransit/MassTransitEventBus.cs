using Funzone.BuildingBlocks.Infrastructure.EventBus;
using MassTransit;
using System.Threading.Tasks;

namespace Funzone.BuildingBlocks.EventBus.MassTransit
{
    public class MassTransitEventBus : IEventBus
    {
        private readonly IBusControl _busControl;
        private readonly IBus _bus;

        public MassTransitEventBus(IBusControl busControl, IBus bus)
        {
            _busControl = busControl;
            _bus = bus;
        }

        public Task Publish<T>(T @event) where T : IntegrationEvent
        {
            return _busControl.Publish(@event);
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler
        {
        }

        public void StartConsuming()
        {
        }
    }
}