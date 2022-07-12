namespace Notex.Core.Domain.SeedWork;

public interface IMemento
{
    Guid SourcedId { get; }
    int Version { get; }
}