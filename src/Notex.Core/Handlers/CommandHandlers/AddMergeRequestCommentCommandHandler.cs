using Notex.Core.Domain.Comments;
using Notex.Core.Domain.MergeRequests;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Identity;
using Notex.Messages.Comments.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class AddMergeRequestCommentCommandHandler : ICommandHandler<AddMergeRequestCommentCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly IEventSourcedRepository<Comment> _commentRepository;
    private readonly IEventSourcedRepository<MergeRequest> _mergeRequestRepository;

    public AddMergeRequestCommentCommandHandler(
        ICurrentUser currentUser, 
        IEventSourcedRepository<Comment> commentRepository,
        IEventSourcedRepository<MergeRequest> mergeRequestRepository)
    {
        _currentUser = currentUser;
        _commentRepository = commentRepository;
        _mergeRequestRepository = mergeRequestRepository;
    }

    public async Task<Guid> Handle(AddMergeRequestCommentCommand request, CancellationToken cancellationToken)
    {
        var mergeRequest = await _mergeRequestRepository.GetAsync(request.MergeRequestId,
            cancellationToken);

        var comment = mergeRequest.AddComment(_currentUser.Id, request.Text);

        await _commentRepository.SaveAsync(comment, cancellationToken);

        return comment.Id;
    }
}