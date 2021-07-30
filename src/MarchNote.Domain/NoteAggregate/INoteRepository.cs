using MarchNote.Domain.SeedWork.Aggregates;

namespace MarchNote.Domain.NoteAggregate
{
    public interface INoteRepository : IAggregateRepository<Note, NoteId>
    {
    }
}