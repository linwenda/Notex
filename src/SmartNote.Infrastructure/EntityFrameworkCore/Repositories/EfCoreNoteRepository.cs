using MediatR;
using SmartNote.Domain.Notes;

namespace SmartNote.Infrastructure.EntityFrameworkCore.Repositories;

public class EfCoreNoteRepository : EfCoreAggregateRootRepository<Note, NoteId>, INoteRepository
{
    public EfCoreNoteRepository(SmartNoteDbContext context, IMediator mediator) : base(mediator, context)
    {
    }
}