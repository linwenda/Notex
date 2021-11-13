using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Spaces.Queries;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace MarchNote.Application.Spaces.Handlers
{
    public class SpaceQueryHandler :
        IQueryHandler<GetDefaultSpacesQuery, IEnumerable<SpaceDto>>,
        IQueryHandler<GetFolderSpacesQuery, IEnumerable<SpaceDto>>,
        IQueryHandler<GetSpaceByIdQuery, SpaceDto>
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

        public async Task<IEnumerable<SpaceDto>> Handle(GetDefaultSpacesQuery request,
            CancellationToken cancellationToken)
        {
            return await _spaceRepository.Queryable
                .Where(u => u.AuthorId == _userContext.UserId)
                .Where(s => s.Type == SpaceType.Default)
                .OrderByDescending(s => s.CreationTime)
                .ProjectTo<SpaceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<SpaceDto>> Handle(GetFolderSpacesQuery request,
            CancellationToken cancellationToken)
        {
            return await _spaceRepository.Queryable
                .Where(s => s.ParentId == request.SpaceId)
                .Where(s => s.AuthorId == _userContext.UserId)
                .ProjectTo<SpaceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<SpaceDto> Handle(GetSpaceByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _spaceRepository.Queryable
                .Where(s => s.Id == request.SpaceId)
                .ProjectTo<SpaceDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}