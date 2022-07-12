namespace Notex.Messages.MergeRequests.Commands;

public class MergeTheRequestCommand : ICommand
{
    public Guid MergeRequestId { get; }

    public MergeTheRequestCommand(Guid mergeRequestId)
    {
        MergeRequestId = mergeRequestId;
    }
}