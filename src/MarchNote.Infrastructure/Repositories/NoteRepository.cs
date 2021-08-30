using MarchNote.Domain.Notes;
using MediatR;

namespace MarchNote.Infrastructure.Repositories
{
    public class NoteRepository : EventSourcedRepository<Note, NoteId>, INoteRepository
    {
        public NoteRepository(MarchNoteDbContext context, IMediator mediator) : base(context, mediator)
        {
        }
    }
}