using System.Threading.Tasks;

namespace Funzone.BuildingBlocks.Infrastructure
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
