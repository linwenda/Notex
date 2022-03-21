using System;
using System.Threading.Tasks;
using Notex.Core.Aggregates.Notes;
using Notex.Core.Queries;
using Notex.IntegrationTests.Notes;
using Notex.Messages.Comments.Commands;
using Xunit;

namespace Notex.IntegrationTests.Comments;

[Collection(IntegrationCollection.Application)]
public class CommentTests : IClassFixture<IntegrationFixture>
{
    private readonly IntegrationFixture _fixture;
    private readonly ICommentQuery _commentQuery;
    
    public CommentTests(IntegrationFixture fixture)
    {
        _fixture = fixture;
        _commentQuery = _fixture.GetService<ICommentQuery>();
    }

    [Fact]
    public async Task AddComment_IsSuccessful()
    {
        var noteId = await _fixture.CreateDefaultNoteAsync(new NoteOptions());

        var command = new AddNoteCommentCommand
        {
            NoteId = noteId,
            Text = "text"
        };
        var commentId = await _fixture.Mediator.Send(command);

        var comment = await _commentQuery.GetCommentAsync(commentId);
        Assert.Equal(command.Text, comment.Text);
        Assert.Equal(command.NoteId.ToString(), comment.EntityId);
        Assert.Equal(nameof(Note), comment.EntityType);
    }

    [Fact]
    public async Task EditComment_IsSuccessful()
    {
        var commentId = await CreateDefaultCommentAsync();

        var command = new EditCommentCommand(commentId, "edit text");

        await _fixture.Mediator.Send(command);

        var comment = await _commentQuery.GetCommentAsync(commentId);
        Assert.Equal(command.Text, comment.Text);
    }

    [Fact]
    public async Task AddCommentReply_IsSuccessful()
    {
        var commentId = await CreateDefaultCommentAsync();

        var command = new AddCommentReplyCommand(commentId, "reply text");

        var replyCommentId = await _fixture.Mediator.Send(command);
        
        var replyComment = await _commentQuery.GetCommentAsync(replyCommentId);
        Assert.Equal(command.Text, replyComment.Text);
        Assert.Equal(commentId, replyComment.RepliedCommentId);
    }

    [Fact]
    public async Task DeleteComment_IsSuccessful()
    {  
        var commentId = await CreateDefaultCommentAsync();

        await _fixture.Mediator.Send(new DeleteCommentCommand(commentId));

        var comment = await _commentQuery.GetCommentAsync(commentId);

        Assert.Null(comment);
    }

    private async Task<Guid> CreateDefaultCommentAsync()
    {
        var noteId = await _fixture.CreateDefaultNoteAsync(new NoteOptions());

        return await _fixture.Mediator.Send(new AddNoteCommentCommand
        {
            NoteId = noteId,
            Text = "text"
        });
    }
}