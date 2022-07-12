using System;
using System.Threading.Tasks;
using MediatR;
using Notex.Core.Domain.Notes;
using Notex.IntegrationTests.Notes;
using Notex.Messages.Comments.Commands;
using Notex.Messages.Comments.Queries;
using Xunit;

namespace Notex.IntegrationTests.Comments;

[Collection("Sequence")]
public class CommentTests : IClassFixture<StartupFixture>
{
    private readonly IMediator _mediator;
    private readonly TestHelper _helper;
    
    public CommentTests(StartupFixture fixture)
    {
        _mediator = fixture.GetService<IMediator>();
        _helper = fixture.GetService<TestHelper>();
    }

    [Fact]
    public async Task AddComment_IsSuccessful()
    {
        var noteId = await _helper.CreateDefaultNoteAsync(new NoteOptions());

        var command = new AddNoteCommentCommand
        {
            NoteId = noteId,
            Text = "text"
        };
        
        var commentId = await _mediator.Send(command);
        var comment = await _mediator.Send(new GetCommentQuery(commentId));
        
        Assert.Equal(command.Text, comment.Text);
        Assert.Equal(command.NoteId.ToString(), comment.EntityId);
        Assert.Equal(nameof(Note), comment.EntityType);
    }

    [Fact]
    public async Task EditComment_IsSuccessful()
    {
        var commentId = await CreateDefaultCommentAsync();

        var command = new EditCommentCommand(commentId, "edit text");

        await _mediator.Send(command);

        var comment = await _mediator.Send(new GetCommentQuery(commentId));
        Assert.Equal(command.Text, comment.Text);
    }

    [Fact]
    public async Task AddCommentReply_IsSuccessful()
    {
        var commentId = await CreateDefaultCommentAsync();

        var command = new AddCommentReplyCommand(commentId, "reply text");

        var replyCommentId = await _mediator.Send(command);

        var replyComment = await _mediator.Send(new GetCommentQuery(replyCommentId));
        Assert.Equal(command.Text, replyComment.Text);
        Assert.Equal(commentId, replyComment.RepliedCommentId);
    }

    [Fact]
    public async Task DeleteComment_IsSuccessful()
    {  
        var commentId = await CreateDefaultCommentAsync();

        await _mediator.Send(new DeleteCommentCommand(commentId));

        var comment = await _mediator.Send(new GetCommentQuery(commentId));

        Assert.True(comment.IsDeleted);
    }

    private async Task<Guid> CreateDefaultCommentAsync()
    {
        var noteId = await _helper.CreateDefaultNoteAsync(new NoteOptions());

        return await _mediator.Send(new AddNoteCommentCommand
        {
            NoteId = noteId,
            Text = "text"
        });
    }
}