using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Domain.MergeRequests.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.MergeRequests;
using Notex.Messages.MergeRequests.Queries;

namespace Notex.Core.Handlers.QueryHandlers;

public class GetNoteMergeRequestsQueryHandler : IQueryHandler<GetNoteMergeRequestsQuery, IEnumerable<MergeRequestDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<MergeRequestDetail> _repository;

    public GetNoteMergeRequestsQueryHandler(IMapper mapper, IReadOnlyRepository<MergeRequestDetail> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<MergeRequestDto>> Handle(GetNoteMergeRequestsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.Query()
            .Where(m => m.DestinationNoteId == request.NoteId)
            .ProjectTo<MergeRequestDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}