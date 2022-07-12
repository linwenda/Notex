using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Domain.Comments.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Comments;
using Notex.Messages.Comments.Queries;

namespace Notex.Core.Handlers.QueryHandlers;

public class GetCommentQueryHandler : IQueryHandler<GetCommentQuery, CommentDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<CommentDetail> _repository;

    public GetCommentQueryHandler(IMapper mapper, IReadOnlyRepository<CommentDetail> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<CommentDto> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Query()
            .Where(c => c.Id == request.CommentId)
            .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}