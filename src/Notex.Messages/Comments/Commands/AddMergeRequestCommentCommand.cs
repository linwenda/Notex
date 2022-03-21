using System;

namespace Notex.Messages.Comments.Commands;

public class AddMergeRequestCommentCommand : ICommand<Guid>
{
    public Guid MergeRequestId { get; }
    public string Text { get; }

    public AddMergeRequestCommentCommand(Guid mergeRequestId, string text)
    {
        MergeRequestId = mergeRequestId;
        Text = text;
    }
}