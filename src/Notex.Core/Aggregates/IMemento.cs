namespace Notex.Core.Aggregates;

public interface IMemento
{
    Guid AggregateId { get; }
    int AggregateVersion { get; }
}