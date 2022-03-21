using Notex.Core.Configuration;
using Notex.Messages.Comments.Commands;

namespace Notex.Core.Aggregates.Comments.CommandHandlers;

public class AddCommentReplyCommandHandler : ICommandHandler<AddCommentReplyCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAggregateRepository _aggregateRepository;

    public AddCommentReplyCommandHandler(ICurrentUser currentUser, IAggregateRepository aggregateRepository)
    {
        _currentUser = currentUser;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Guid> Handle(AddCommentReplyCommand request, CancellationToken cancellationToken)
    {
        var comment = await _aggregateRepository.LoadAsync<Comment>(request.CommentId);

        var replyComment = comment.Reply(_currentUser.Id, request.Text);

        await _aggregateRepository.SaveAsync(replyComment);

        return replyComment.Id;
    }
}