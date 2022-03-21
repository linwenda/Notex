using Microsoft.EntityFrameworkCore;
using Notex.Core.Aggregates;
using Notex.Core.Aggregates.MergeRequests.ReadModel;
using Notex.Core.Lifetimes;

namespace Notex.Core.Queries;

public class MergeRequestQuery : IMergeRequestQuery, IScopedLifetime
{
    private readonly IReadModelRepository _readModelRepository;

    public MergeRequestQuery(IReadModelRepository readModelRepository)
    {
        _readModelRepository = readModelRepository;
    }

    public async Task<MergeRequestDetail> GetMergeRequestAsync(Guid id)
    {
        return await _readModelRepository.Query<MergeRequestDetail>().FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<MergeRequestDetail>> GetMergeRequestsFromNoteAsync(Guid noteId)
    {
        return await _readModelRepository.Query<MergeRequestDetail>()
            .Where(m => m.DestinationNoteId == noteId)
            .ToListAsync();
    }
}