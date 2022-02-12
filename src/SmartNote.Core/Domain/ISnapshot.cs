namespace SmartNote.Core.Domain
{
    public interface ISnapshot
    {
    }

    public interface ISnapshot<out TKey> : ISnapshot where TKey : IEventSourcedAggregateKey
    {
        TKey AggregateRootId { get; }
        int AggregateRootVersion { get; }
    }
}