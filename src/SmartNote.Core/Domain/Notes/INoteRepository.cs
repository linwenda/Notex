namespace SmartNote.Core.Domain.Notes
{
    public interface INoteRepository : IAggregateRootRepository<Note, NoteId>
    {
    }
}