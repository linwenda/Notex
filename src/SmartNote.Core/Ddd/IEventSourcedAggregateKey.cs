namespace SmartNote.Core.Ddd;

public interface IEventSourcedAggregateKey
{
    Guid Value { get; }
}