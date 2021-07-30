using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Domain.NoteAggregate.ReadModels;
using MarchNote.Domain.SeedWork.Aggregates;
using MarchNote.Domain.Users;

namespace MarchNote.Application.Notes.Queries
{
    public class NoteQueryHandler :
        IQueryHandler<GetNoteQuery, MarchNoteResponse<NoteReadModel>>,
        IQueryHandler<GetNotesQuery, MarchNoteResponse<IEnumerable<NoteReadModel>>>,
        IQueryHandler<GetNoteHistoriesQuery, MarchNoteResponse<IEnumerable<NoteHistoryReadModel>>>
    {
        private readonly IUserContext _userContext;
        private readonly IReadModelRepository<NoteReadModel> _noteRepository;
        private readonly IReadModelRepository<NoteHistoryReadModel> _noteHistoryRepository;

        public NoteQueryHandler(
            IUserContext userContext,
            IReadModelRepository<NoteReadModel> noteRepository,
            IReadModelRepository<NoteHistoryReadModel> noteHistoryRepository)
        {
            _userContext = userContext;
            _noteRepository = noteRepository;
            _noteHistoryRepository = noteHistoryRepository;
        }

        public async Task<MarchNoteResponse<NoteReadModel>> Handle(GetNoteQuery request, CancellationToken cancellationToken)
        {
            return new MarchNoteResponse<NoteReadModel>(
                await _noteRepository.FirstOrDefaultAsync(n => n.Id == request.NoteId));
        }

        public async Task<MarchNoteResponse<IEnumerable<NoteReadModel>>> Handle(GetNotesQuery request,
            CancellationToken cancellationToken)
        {
            return new MarchNoteResponse<IEnumerable<NoteReadModel>>(
                await _noteRepository.ListAsync(n => n.AuthorId == _userContext.UserId.Value));
        }

        public async Task<MarchNoteResponse<IEnumerable<NoteHistoryReadModel>>> Handle(GetNoteHistoriesQuery request,
            CancellationToken cancellationToken)
        {
            return new MarchNoteResponse<IEnumerable<NoteHistoryReadModel>>(await _noteHistoryRepository.ListAsync(
                n => n.NoteId == request.NoteId));
        }
    }
}