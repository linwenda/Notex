using Microsoft.EntityFrameworkCore;
using Notex.Core.Aggregates;
using Notex.Core.Aggregates.Comments.ReadModels;
using Notex.Core.Lifetimes;

namespace Notex.Core.Queries;

public class CommentQuery : ICommentQuery, IScopedLifetime
{
    private readonly IReadModelRepository _readModelRepository;

    public CommentQuery(IReadModelRepository readModelRepository)
    {
        _readModelRepository = readModelRepository;
    }

    public async Task<CommentDetail> GetCommentAsync(Guid id)
    {
        return await _readModelRepository.Query<CommentDetail>().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<CommentDetail>> GetCommentsAsync(string entityType, string entityId)
    {
        return await _readModelRepository.Query<CommentDetail>()
            .Where(c => c.EntityType == entityType && c.EntityId == entityId)
            .ToListAsync();
    }
}