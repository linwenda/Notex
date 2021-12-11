namespace SmartNote.Core.Domain
{
    public interface ICanSoftDelete
    {
        bool IsDeleted { get; set; }
    }
}