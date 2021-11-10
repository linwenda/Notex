using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.NoteCooperations.Queries;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace MarchNote.Application.NoteCooperations.Handlers
{
    public class NoteCooperationQueryHandler :
        IQueryHandler<GetUserNoteCooperationsQuery, IEnumerable<NoteCooperationDto>>,
        IQueryHandler<GetNoteCooperationsQuery, IEnumerable<NoteCooperationDto>>,
        IQueryHandler<GetNoteCooperationByIdQuery, NoteCooperationDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IRepository<NoteCooperation> _cooperationRepository;

        public NoteCooperationQueryHandler(
            IMapper mapper,
            IUserContext userContext,
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
                .Where(c => c.NoteId == new NoteId(request.NoteId))
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
                .Where(c => c.SubmitterId == _userContext.UserId)
                .ProjectTo<NoteCooperationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}