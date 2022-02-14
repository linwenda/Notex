namespace SmartNote.Core.Ddd
{
    public abstract class Snapshot<TKey> : ISnapshot<TKey> where TKey : IEventSourcedAggregateKey
    {
        protected Snapshot(TKey aggregateId, int aggregateVersion)
        {
            AggregateRootId = aggregateId;
            AggregateRootVersion = aggregateVersion;
        }

        public TKey AggregateRootId { get; }
        public int AggregateRootVersion { get; }
    }
}