namespace Notex.Messages.MergeRequests.Queries;

public class GetMergeRequestQuery : IQuery<MergeRequestDto>
{
    public Guid MergeRequestId { get; }

    public GetMergeRequestQuery(Guid mergeRequestId)
    {
        MergeRequestId = mergeRequestId;
    }
}