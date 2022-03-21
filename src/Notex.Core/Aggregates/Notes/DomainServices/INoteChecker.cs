namespace Notex.Core.Aggregates.Notes.DomainServices;

public interface INoteChecker
{
    bool IsPublishedNote(Guid noteId);
}