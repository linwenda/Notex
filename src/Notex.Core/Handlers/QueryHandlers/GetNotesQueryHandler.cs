using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Domain.Notes.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Queries;

namespace Notex.Core.Handlers.QueryHandlers;

public class GetNotesQueryHandler : IQueryHandler<GetNotesQuery, GetNotesResponse>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<NoteDetail> _repository;

    public GetNotesQueryHandler(IMapper mapper, IReadOnlyRepository<NoteDetail> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<GetNotesResponse> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        var source = _repository.Query().Where(n => n.SpaceId == request.SpaceId);

        var count = await source.CountAsync(cancellationToken);

        var items = await source.OrderByDescending(n => n.CreationTime)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<NoteDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetNotesResponse
        {
            PageIndex = request.PageIndex,
            TotalCount = count,
            Data = items
        };
    }
}