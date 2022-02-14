namespace SmartNote.Core.Ddd;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}