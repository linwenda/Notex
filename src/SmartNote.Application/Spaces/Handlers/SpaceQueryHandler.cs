using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartNote.Application.Configuration.Queries;
using SmartNote.Application.Configuration.Security.Users;
using SmartNote.Application.Spaces.Commands;
using SmartNote.Application.Spaces.Queries;
using SmartNote.Domain;
using SmartNote.Domain.Spaces;

namespace SmartNote.Application.Spaces.Handlers
{
    public class SpaceQueryHandler :
        IQueryHandler<GetDefaultSpacesQuery, IEnumerable<SpaceDto>>,
        IQueryHandler<GetFolderSpacesQuery, IEnumerable<SpaceDto>>,
        IQueryHandler<GetSpaceByIdQuery, SpaceDto>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<Space> _spaceRepository;

        public SpaceQueryHandler(
            IMapper mapper,
            ICurrentUser userContext,
            IRepository<Space> spaceRepository)
        {
            _mapper = mapper;
            _currentUser = userContext;
            _spaceRepository = spaceRepository;
        }

        public async Task<IEnumerable<SpaceDto>> Handle(GetDefaultSpacesQuery request,
            CancellationToken cancellationToken)
        {
            return await _spaceRepository.Queryable
                .Where(u => u.AuthorId == _currentUser.Id)
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
                .Where(s => s.AuthorId == _currentUser.Id)
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