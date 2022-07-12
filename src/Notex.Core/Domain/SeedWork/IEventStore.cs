using Notex.Messages;

namespace Notex.Core.Domain.SeedWork;

public interface IEventStore
{
    Task<IEnumerable<IVersionedEvent>> LoadAsync(Guid sourcedId, int minVersion,
        CancellationToken cancellationToken = default);

    Task SaveAsync(IEnumerable<IVersionedEvent> events, CancellationToken cancellationToken = default);
}