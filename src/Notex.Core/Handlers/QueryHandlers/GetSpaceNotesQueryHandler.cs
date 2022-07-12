using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Domain.Notes.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Queries;

namespace Notex.Core.Handlers.QueryHandlers;

public class GetSpaceNotesQueryHandler : IQueryHandler<GetSpaceNotesQuery, IEnumerable<NoteDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<NoteDetail> _repository;

    public GetSpaceNotesQueryHandler(IMapper mapper,IReadOnlyRepository<NoteDetail> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<NoteDto>> Handle(GetSpaceNotesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Query()
            .Where(n => n.SpaceId == request.SpaceId)
            .ProjectTo<NoteDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}