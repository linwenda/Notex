namespace Notex.Core.Aggregates.MergeRequests.DomainServices;

public interface IMergeRequestChecker
{
    bool HasOpenMergeRequest(Guid sourceNoteId, Guid destinationNoteId);
}