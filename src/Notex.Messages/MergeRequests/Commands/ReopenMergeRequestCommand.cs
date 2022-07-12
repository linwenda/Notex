namespace Notex.Messages.MergeRequests.Commands;

public class ReopenMergeRequestCommand : ICommand
{
    public Guid MergeRequestId { get; }

    public ReopenMergeRequestCommand(Guid mergeRequestId)
    {
        MergeRequestId = mergeRequestId;
    }
}