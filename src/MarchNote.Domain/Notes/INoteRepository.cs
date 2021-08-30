using MarchNote.Domain.SeedWork.EventSourcing;

namespace MarchNote.Domain.Notes
{
    public interface INoteRepository : IEventSourcedRepository<Note, NoteId>
    {
    }
}