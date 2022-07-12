using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Domain.Comments.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Comments;
using Notex.Messages.Comments.Queries;

namespace Notex.Core.Handlers.QueryHandlers;

public class GetCommentsQueryHandler : IQueryHandler<GetCommentsQuery, IEnumerable<CommentDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<CommentDetail> _repository;

    public GetCommentsQueryHandler(IMapper mapper,IReadOnlyRepository<CommentDetail> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<CommentDto>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Query()
            .Where(c => c.EntityType == request.EntityType && c.EntityId == request.EntityId)
            .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}