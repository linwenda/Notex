using SmartNote.Core.Ddd;

namespace SmartNote.Domain.Notes
{
    public interface INoteRepository : IEventSourcedRepository<Note, NoteId>
    {
    }
}