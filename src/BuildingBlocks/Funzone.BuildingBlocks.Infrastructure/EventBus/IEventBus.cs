using System.Threading.Tasks;

namespace Funzone.BuildingBlocks.Infrastructure.EventBus
{
    public interface IEventBus
    {
        Task Publish<TIntegrationEvent>(TIntegrationEvent @event)
            where TIntegrationEvent : IntegrationEvent;
    }
}