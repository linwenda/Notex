namespace SmartNote.Domain
{
    public interface ICanSoftDelete
    {
        bool IsDeleted { get; set; }
    }
}