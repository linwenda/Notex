using System;
using Notex.Core.Aggregates.Comments;
using Notex.Core.Aggregates.MergeRequests;
using Notex.Core.Aggregates.Notes;
using Notex.Messages.Comments.Events;
using Notex.Messages.Notes;
using Notex.UnitTests.MergeRequests;
using Notex.UnitTests.Notes;
using Xunit;

namespace Notex.UnitTests.Comments;

public class CommentTests
{
    [Fact]
    public void AddComment_WithNote_IsSuccessful()
    {
        var note = NoteTestHelper.CreateNote(new NoteOptions { Status = NoteStatus.Published });

        var comment = note.AddComment(Guid.NewGuid(), "comment text");

        var @event = comment.PopUncommittedEvents().Have<CommentCreatedEvent>();

        Assert.Equal(nameof(Note), @event.EntityType);
        Assert.Equal(note.Id.ToString(), @event.EntityId);
        Assert.Equal("comment text", @event.Text);
        Assert.Null(@event.RepliedCommentId);
    }

    [Fact]
    public void AddComment_WithMergeRequest_IsSuccessful()
    {
        var mergeRequest = MergeRequestTestHelper.CreateOpenMergeRequest();

        var comment = mergeRequest.AddComment(Guid.NewGuid(), "merge request text");

        var @event = comment.PopUncommittedEvents().Have<CommentCreatedEvent>();

        Assert.Equal(nameof(MergeRequest), @event.EntityType);
        Assert.Equal(mergeRequest.Id.ToString(), @event.EntityId);
        Assert.Equal("merge request text", @event.Text);
        Assert.Null(@event.RepliedCommentId);
    }

    [Fact]
    public void Edit_IsSuccessful()
    {
        var comment = CreateDefaultComment();

        comment.Edit("Interesting point of view");

        var @event = comment.PopUncommittedEvents().Have<CommentEditedEvent>();

        Assert.Equal("Interesting point of view", @event.Text);
    }

    [Fact]
    public void Reply_IsSuccessful()
    {
        var comment = CreateDefaultComment();

        var input = new
        {
            UserId = Guid.NewGuid(),
            ReplyText = "reply"
        };

        var replyComment = comment.Reply(input.UserId, input.ReplyText);

        var commentCreatedEvent = comment.PopUncommittedEvents().Have<CommentCreatedEvent>();
        var commentCreatedEventForReply = replyComment.PopUncommittedEvents().Have<CommentCreatedEvent>();

        Assert.Equal(commentCreatedEvent.AggregateId, commentCreatedEventForReply.RepliedCommentId);
        Assert.Equal(commentCreatedEvent.EntityType, commentCreatedEventForReply.EntityType);
        Assert.Equal(commentCreatedEvent.EntityId, commentCreatedEventForReply.EntityId);
        Assert.Equal(input.UserId, commentCreatedEventForReply.CreatorId);
        Assert.Equal(input.ReplyText, commentCreatedEventForReply.Text);
    }

    private static Comment CreateDefaultComment()
    {
        var note = NoteTestHelper.CreateNote(new NoteOptions { Status = NoteStatus.Published });

        return note.AddComment(Guid.NewGuid(), "comment text");
    }
}