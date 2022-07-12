using Notex.Messages;

namespace Notex.Core.Domain.SeedWork;

public interface IEventSourced
{
    Guid Id { get; }
    int Version { get; }
    IReadOnlyCollection<IVersionedEvent> PopUncommittedEvents();
}