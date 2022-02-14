namespace SmartNote.Core.Ddd
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