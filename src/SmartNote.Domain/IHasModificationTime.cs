namespace SmartNote.Domain
{
    public interface IHasModificationTime
    {
        DateTimeOffset? LastModificationTime { get; set; }
    }
}