using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Domain.Notes.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Queries;

namespace Notex.Core.Handlers.QueryHandlers;

public class GetNoteQueryHandler : IQueryHandler<GetNoteQuery, NoteDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<NoteDetail> _repository;

    public GetNoteQueryHandler(IMapper mapper, IReadOnlyRepository<NoteDetail> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<NoteDto> Handle(GetNoteQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Query()
            .Where(n => n.Id == request.NoteId)
            .ProjectTo<NoteDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}