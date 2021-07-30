using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.NoteCooperations.Queries;
using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace MarchNote.Application.NoteCooperations.Handlers
{
    public class NoteCooperationQueryHandler :
        IQueryHandler<GetUserNoteCooperationsQuery,MarchNoteResponse<IEnumerable<NoteCooperationDto>>>,
        IQueryHandler<GetNoteCooperationsQuery, MarchNoteResponse<IEnumerable<NoteCooperationDto>>>,
        IQueryHandler<GetNoteCooperationByIdQuery, MarchNoteResponse<NoteCooperationDto>>
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

        public async Task<MarchNoteResponse<IEnumerable<NoteCooperationDto>>> Handle(GetNoteCooperationsQuery request,
            CancellationToken cancellationToken)
        {
            var cooperations = await _cooperationRepository.Entities
                .Where(c => c.NoteId == new NoteId(request.NoteId))
                .ProjectTo<NoteCooperationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MarchNoteResponse<IEnumerable<NoteCooperationDto>>(cooperations);
        }

        public async Task<MarchNoteResponse<NoteCooperationDto>> Handle(GetNoteCooperationByIdQuery request,
            CancellationToken cancellationToken)
        {
            var cooperation = await _cooperationRepository.Entities
                .Where(c => c.Id == new NoteCooperationId(request.CooperationId))
                .ProjectTo<NoteCooperationDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return new MarchNoteResponse<NoteCooperationDto>(cooperation);
        }

        public async Task<MarchNoteResponse<IEnumerable<NoteCooperationDto>>> Handle(GetUserNoteCooperationsQuery request, CancellationToken cancellationToken)
        {
            var cooperations = await _cooperationRepository.Entities
                .Where(c => c.SubmitterId == _userContext.UserId)
                .ProjectTo<NoteCooperationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MarchNoteResponse<IEnumerable<NoteCooperationDto>>(cooperations);
        }
    }
}