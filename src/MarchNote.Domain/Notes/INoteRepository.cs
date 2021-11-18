using MarchNote.Domain.Shared.EventSourcing;

namespace MarchNote.Domain.Notes
{
    public interface INoteRepository : IAggregateRootRepository<Note, NoteId>
    {
    }
}