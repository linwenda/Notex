namespace Notex.Core.Aggregates;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}