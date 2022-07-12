using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Domain.Spaces.ReadModels;
using Notex.Messages.Spaces;
using Notex.Messages.Spaces.Queries;

namespace Notex.Core.Handlers.QueryHandlers;

public class GetSpaceQueryHandler : IQueryHandler<GetSpaceQuery, SpaceDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<SpaceDetail> _repository;

    public GetSpaceQueryHandler(IMapper mapper, IReadOnlyRepository<SpaceDetail> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public Task<SpaceDto> Handle(GetSpaceQuery request, CancellationToken cancellationToken)
    {
        return _repository.Query()
            .Where(s => s.Id == request.SpaceId)
            .ProjectTo<SpaceDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}