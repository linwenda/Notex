namespace SmartNote.Domain.Notes
{
    public interface INoteRepository : IAggregateRootRepository<Note, NoteId>
    {
    }
}