namespace SmartNote.Domain
{
    public interface ISnapshot
    {
        Guid AggregateId { get; }
        int AggregateVersion { get; }
    }
}