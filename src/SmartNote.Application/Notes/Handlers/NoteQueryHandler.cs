using AutoMapper;
using SmartNote.Application.Configuration.Queries;
using SmartNote.Application.Configuration.Security.Users;
using SmartNote.Application.Notes.Queries;
using SmartNote.Domain;
using SmartNote.Domain.Notes.ReadModels;

namespace SmartNote.Application.Notes.Handlers
{
    public class NoteQueryHandler :
        IQueryHandler<GetNoteQuery, NoteDto>,
        IQueryHandler<GetNotesQuery, IEnumerable<NoteReadModel>>,
        IQueryHandler<GetNoteHistoriesQuery, IEnumerable<NoteHistoryReadModel>>,
        IQueryHandler<GetNotesBySpaceIdQuery, IEnumerable<NoteReadModel>>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<NoteReadModel> _noteRepository;
        private readonly IRepository<NoteHistoryReadModel> _noteHistoryRepository;

        public NoteQueryHandler(
            IMapper mapper,
            ICurrentUser currentUser,
            IRepository<NoteReadModel> noteRepository,
            IRepository<NoteHistoryReadModel> noteHistoryRepository)
        {
            _mapper = mapper;
            _currentUser = currentUser;
            _noteRepository = noteRepository;
            _noteHistoryRepository = noteHistoryRepository;
        }

        public async Task<NoteDto> Handle(GetNoteQuery request,
            CancellationToken cancellationToken)
        {
            var note = await _noteRepository.FirstOrDefaultAsync(n => n.Id == request.NoteId);

            return _mapper.Map<NoteDto>(note);
        }

        public async Task<IEnumerable<NoteReadModel>> Handle(GetNotesQuery request,
            CancellationToken cancellationToken)
        {
            return await _noteRepository.ListAsync(n => n.AuthorId == _currentUser.Id);
        }

        public async Task<IEnumerable<NoteHistoryReadModel>> Handle(GetNoteHistoriesQuery request,
            CancellationToken cancellationToken)
        {
            return await _noteHistoryRepository.ListAsync(
                n => n.NoteId == request.NoteId);
        }

        public async Task<IEnumerable<NoteReadModel>> Handle(GetNotesBySpaceIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _noteRepository.ListAsync(n => n.SpaceId == request.SpaceId);
        }
    }
}