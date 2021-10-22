using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.Spaces.Queries;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace MarchNote.Application.Spaces.Handlers
{
    public class SpaceQueryHandler :
        IQueryHandler<GetDefaultSpacesQuery, MarchNoteResponse<IEnumerable<SpaceDto>>>,
        IQueryHandler<GetFolderSpacesQuery, MarchNoteResponse<IEnumerable<SpaceDto>>>,
        IQueryHandler<GetSpaceByIdQuery, MarchNoteResponse<SpaceDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly IRepository<Space> _spaceRepository;

        public SpaceQueryHandler(
            IMapper mapper,
            IUserContext userContext,
            IRepository<Space> spaceRepository)
        {
            _mapper = mapper;
            _userContext = userContext;
            _spaceRepository = spaceRepository;
        }

        public async Task<MarchNoteResponse<IEnumerable<SpaceDto>>> Handle(GetDefaultSpacesQuery request,
            CancellationToken cancellationToken)
        {
            var spaces = await _spaceRepository.Entities
                .Where(u => u.AuthorId == _userContext.UserId)
                .Where(s => s.Type == SpaceType.Default)
                .OrderByDescending(s => s.CreatedAt)
                .ProjectTo<SpaceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MarchNoteResponse<IEnumerable<SpaceDto>>(spaces);
        }

        public async Task<MarchNoteResponse<IEnumerable<SpaceDto>>> Handle(GetFolderSpacesQuery request,
            CancellationToken cancellationToken)
        {
            var spaceFolders = await _spaceRepository.Entities
                .Where(s => s.ParentId == new SpaceId(request.SpaceId))
                .Where(s => s.AuthorId == _userContext.UserId)
                .ProjectTo<SpaceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MarchNoteResponse<IEnumerable<SpaceDto>>(spaceFolders);
        }

        public async Task<MarchNoteResponse<SpaceDto>> Handle(GetSpaceByIdQuery request,
            CancellationToken cancellationToken)
        {
            var space = await _spaceRepository.Entities
                .Where(s => s.Id == new SpaceId(request.SpaceId))
                .ProjectTo<SpaceDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return new MarchNoteResponse<SpaceDto>(space);
        }
    }
}