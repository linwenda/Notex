using Notex.Core.DependencyInjection;
using Notex.Core.Domain.Notes;

namespace Notex.Core.Domain.MergeRequests;

public interface IMergeRequestService : IScopedLifetime
{
    Task<MergeRequest> CreateMergeRequestAsync(Note note, Guid userId, string title, string description);
    void ReOpenMergeRequest(MergeRequest mergeRequest, Note destinationNote, Guid userId);
    void Merge(MergeRequest mergeRequest, Note sourceNote, Note destinationNote, Guid userId);
}