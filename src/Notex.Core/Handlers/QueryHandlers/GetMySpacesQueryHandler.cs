using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Domain.Spaces.ReadModels;
using Notex.Core.Identity;
using Notex.Messages.Spaces;
using Notex.Messages.Spaces.Queries;

namespace Notex.Core.Handlers.QueryHandlers;

public class GetMySpacesQueryHandler : IQueryHandler<GetMySpacesQuery, IEnumerable<SpaceDto>>
{
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IReadOnlyRepository<SpaceDetail> _repository;

    public GetMySpacesQueryHandler(
        IMapper mapper,
        ICurrentUser currentUser,
        IReadOnlyRepository<SpaceDetail> repository)
    {
        _mapper = mapper;
        _currentUser = currentUser;
        _repository = repository;
    }

    public async Task<IEnumerable<SpaceDto>> Handle(GetMySpacesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Query()
            .Where(s => s.CreatorId == _currentUser.Id)
            .ProjectTo<SpaceDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(m => m.LastModificationTime)
            .ToListAsync(cancellationToken);
    }
}