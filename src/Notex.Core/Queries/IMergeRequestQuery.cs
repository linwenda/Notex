using Notex.Core.Aggregates.MergeRequests.ReadModel;
using Notex.Core.Queries.Models;

namespace Notex.Core.Queries;

public interface IMergeRequestQuery
{
    Task<MergeRequestDetail> GetMergeRequestAsync(Guid id);
    Task<IEnumerable<MergeRequestDetail>> GetMergeRequestsFromNoteAsync(Guid noteId);
}