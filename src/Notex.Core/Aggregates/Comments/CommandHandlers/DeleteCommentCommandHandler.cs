using MediatR;
using Notex.Core.Authorization;
using Notex.Core.Configuration;
using Notex.Messages.Comments.Commands;

namespace Notex.Core.Aggregates.Comments.CommandHandlers;

public class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand>
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IResourceAuthorizationService _resourceAuthorizationService;

    public DeleteCommentCommandHandler(
        IAggregateRepository aggregateRepository,
        IResourceAuthorizationService resourceAuthorizationService)
    {
        _aggregateRepository = aggregateRepository;
        _resourceAuthorizationService = resourceAuthorizationService;
    }

    public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _aggregateRepository.LoadAsync<Comment>(request.CommentId);

        await _resourceAuthorizationService.CheckAsync(comment, CommonOperations.Delete);

        comment.Delete();

        await _aggregateRepository.SaveAsync(comment);

        return Unit.Value;
    }
}