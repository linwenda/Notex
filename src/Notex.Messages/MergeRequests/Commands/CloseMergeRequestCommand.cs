namespace Notex.Messages.MergeRequests.Commands;

public class CloseMergeRequestCommand : ICommand
{
    public Guid MergeRequestId { get; }

    public CloseMergeRequestCommand(Guid mergeRequestId)
    {
        MergeRequestId = mergeRequestId;
    }
}