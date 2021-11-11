using MarchNote.Domain.Shared.EventSourcing;

namespace MarchNote.Domain.Notes
{
    public interface INoteRepository : IEventSourcedRepository<Note, NoteId>
    {
    }
}