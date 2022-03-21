using System;

namespace Notex.Messages.MergeRequests.Commands;

public class UpdateMergeRequestCommand : ICommand
{
    public Guid MergeRequestId { get; }
    public string Title { get; }
    public string Description { get; }

    public UpdateMergeRequestCommand(Guid mergeRequestId, string title, string description)
    {
        MergeRequestId = mergeRequestId;
        Title = title;
        Description = description;
    }
}