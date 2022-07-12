using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Domain.MergeRequests.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.MergeRequests;
using Notex.Messages.MergeRequests.Queries;

namespace Notex.Core.Handlers.QueryHandlers;

public class GetMergeRequestQueryHandler : IQueryHandler<GetMergeRequestQuery, MergeRequestDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<MergeRequestDetail> _repository;

    public GetMergeRequestQueryHandler(IMapper mapper, IReadOnlyRepository<MergeRequestDetail> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<MergeRequestDto> Handle(GetMergeRequestQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Query()
            .Where(m => m.Id == request.MergeRequestId)
            .ProjectTo<MergeRequestDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}