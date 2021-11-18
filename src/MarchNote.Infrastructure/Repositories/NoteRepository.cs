using MarchNote.Domain.Notes;
using MediatR;

namespace MarchNote.Infrastructure.Repositories
{
    public class NoteRepository : AggregateRootRepository<Note, NoteId>, INoteRepository
    {
        public NoteRepository(MarchNoteDbContext context, IMediator mediator) : base(context, mediator)
        {
        }
    }
}