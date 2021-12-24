namespace SmartNote.Domain
{
    public interface IHasCreationTime
    {
        DateTimeOffset CreationTime { get; set; }
    }
}