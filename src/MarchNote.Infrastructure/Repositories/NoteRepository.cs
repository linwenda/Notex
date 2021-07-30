using MarchNote.Domain.NoteAggregate;
using MediatR;

namespace MarchNote.Infrastructure.Repositories
{
    public class NoteRepository : AggregateRepository<Note, NoteId>, INoteRepository
    {
        public NoteRepository(MarchNoteDbContext context, IMediator mediator) : base(context, mediator)
        {
        }
    }
}