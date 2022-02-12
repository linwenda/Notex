namespace SmartNote.Core.Domain;

public interface IEventSourcedAggregateKey
{
    Guid Value { get; }
}