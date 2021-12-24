namespace SmartNote.Domain
{
    public abstract class Snapshot : ISnapshot
    {
        protected Snapshot(Guid aggregateId, int aggregateVersion)
        {
            AggregateId = aggregateId;
            AggregateVersion = aggregateVersion;
        }

        public Guid AggregateId { get; }
        public int AggregateVersion { get; }
    }
}