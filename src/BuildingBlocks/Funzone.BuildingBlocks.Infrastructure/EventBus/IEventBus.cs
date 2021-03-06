using System.Threading.Tasks;

namespace Funzone.BuildingBlocks.Infrastructure.EventBus
{
    public interface IEventBus
    {
        Task Publish<T>(T @event) where T : IntegrationEvent;

        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler
        {
        }

        void StartConsuming();
    }
}