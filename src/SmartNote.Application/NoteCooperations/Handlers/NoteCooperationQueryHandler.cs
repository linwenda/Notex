using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartNote.Application.Configuration.Queries;
using SmartNote.Application.Configuration.Security.Users;
using SmartNote.Application.NoteCooperations.Queries;
using SmartNote.Domain;
using SmartNote.Domain.NoteCooperations;

namespace SmartNote.Application.NoteCooperations.Handlers
{
    public class NoteCooperationQueryHandler :
        IQueryHandler<GetUserNoteCooperationsQuery, IEnumerable<NoteCooperationDto>>,
        IQueryHandler<GetNoteCooperationsQuery, IEnumerable<NoteCooperationDto>>,
        IQueryHandler<GetNoteCooperationByIdQuery, NoteCooperationDto>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUser _userContext;
        private readonly IRepository<NoteCooperation> _cooperationRepository;

        public NoteCooperationQueryHandler(
            IMapper mapper,
            ICurrentUser userContext,
            IRepository<NoteCooperation> cooperationRepository)
        {
            _mapper = mapper;
            _userContext = userContext;
            _cooperationRepository = cooperationRepository;
        }

        public async Task<IEnumerable<NoteCooperationDto>> Handle(GetNoteCooperationsQuery request,
            CancellationToken cancellationToken)
        {
            return await _cooperationRepository.Queryable
                .Where(c => c.NoteId == request.NoteId)
                .ProjectTo<NoteCooperationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<NoteCooperationDto> Handle(GetNoteCooperationByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _cooperationRepository.Queryable
                .Where(c => c.Id == request.CooperationId)
                .ProjectTo<NoteCooperationDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<NoteCooperationDto>> Handle(
            GetUserNoteCooperationsQuery request, CancellationToken cancellationToken)
        {
            return await _cooperationRepository.Queryable
                .Where(c => c.SubmitterId == _userContext.Id)
                .ProjectTo<NoteCooperationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}