using System.Threading.Tasks;

namespace Funzone.BuildingBlocks.EventBus.Abstractions
{
    public interface IEventBus
    {
        Task Publish<T>(T @event) where T : IntegrationEvent;
    }
}