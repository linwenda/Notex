using Notex.Core.Aggregates.MergeRequests;
using Notex.Core.Configuration;
using Notex.Messages.Comments.Commands;

namespace Notex.Core.Aggregates.Comments.CommandHandlers;

public class AddMergeRequestCommentCommandHandler : ICommandHandler<AddMergeRequestCommentCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAggregateRepository _aggregateRepository;

    public AddMergeRequestCommentCommandHandler(ICurrentUser currentUser, IAggregateRepository aggregateRepository)
    {
        _currentUser = currentUser;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Guid> Handle(AddMergeRequestCommentCommand request, CancellationToken cancellationToken)
    {
        var mergeRequest = await _aggregateRepository.LoadAsync<MergeRequest>(request.MergeRequestId);

        var comment = mergeRequest.AddComment(_currentUser.Id, request.Text);

        await _aggregateRepository.SaveAsync(comment);

        return comment.Id;
    }
}