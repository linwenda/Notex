namespace SmartNote.Core.Domain;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}