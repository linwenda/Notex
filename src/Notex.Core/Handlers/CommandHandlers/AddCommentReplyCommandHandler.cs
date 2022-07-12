using Notex.Core.Domain.Comments;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Identity;
using Notex.Messages.Comments.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class AddCommentReplyCommandHandler : ICommandHandler<AddCommentReplyCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly IEventSourcedRepository<Comment> _commentRepository;

    public AddCommentReplyCommandHandler(ICurrentUser currentUser, IEventSourcedRepository<Comment> commentRepository)
    {
        _currentUser = currentUser;
        _commentRepository = commentRepository;
    }

    public async Task<Guid> Handle(AddCommentReplyCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetAsync(request.CommentId, cancellationToken);

        var replyComment = comment.Reply(_currentUser.Id, request.Text);

        await _commentRepository.SaveAsync(replyComment, cancellationToken);

        return replyComment.Id;
    }
}