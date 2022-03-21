using Notex.Core.Aggregates.MergeRequests.ReadModel;
using Notex.Core.Lifetimes;
using Notex.Messages.MergeRequests;

namespace Notex.Core.Aggregates.MergeRequests.DomainServices;

public class MergeRequestChecker : IMergeRequestChecker, IScopedLifetime
{
    private readonly IReadModelRepository _readModelRepository;

    public MergeRequestChecker(IReadModelRepository readModelRepository)
    {
        _readModelRepository = readModelRepository;
    }

    public bool HasOpenMergeRequest(Guid sourceNoteId, Guid destinationNoteId)
    {
        return _readModelRepository.Query<MergeRequestDetail>().Any(m => m.SourceNoteId == sourceNoteId &&
                                                                         m.DestinationNoteId == destinationNoteId &&
                                                                         m.Status == MergeRequestStatus.Open);
    }
}