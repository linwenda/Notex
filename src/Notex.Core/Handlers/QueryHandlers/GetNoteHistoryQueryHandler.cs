using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Domain.Notes.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Queries;

namespace Notex.Core.Handlers.QueryHandlers;

public class GetNoteHistoryQueryHandler : IQueryHandler<GetNoteHistoryQuery, NoteHistoryDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<NoteHistory> _repository;

    public GetNoteHistoryQueryHandler(IMapper mapper, IReadOnlyRepository<NoteHistory> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<NoteHistoryDto> Handle(GetNoteHistoryQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Query()
            .Where(n => n.Id == request.NoteHistoryId)
            .ProjectTo<NoteHistoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}