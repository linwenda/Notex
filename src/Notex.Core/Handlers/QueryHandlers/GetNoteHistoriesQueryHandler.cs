using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Domain.Notes.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Queries;

namespace Notex.Core.Handlers.QueryHandlers;

public class GetNoteHistoriesQueryHandler : IQueryHandler<GetNoteHistoriesQuery, IEnumerable<NoteHistoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<NoteHistory> _repository;

    public GetNoteHistoriesQueryHandler(IMapper mapper, IReadOnlyRepository<NoteHistory> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<NoteHistoryDto>> Handle(GetNoteHistoriesQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.Query()
            .Where(n => n.NoteId == request.NoteId)
            .ProjectTo<NoteHistoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}