namespace SmartNote.Core.Domain.Notes
{
    public interface INoteRepository : IEventSourcedRepository<Note, NoteId>
    {
    }
}