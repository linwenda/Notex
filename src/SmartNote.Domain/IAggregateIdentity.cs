namespace SmartNote.Domain
{
    public interface IAggregateIdentity
    {
        Guid Value { get; }
    }
}