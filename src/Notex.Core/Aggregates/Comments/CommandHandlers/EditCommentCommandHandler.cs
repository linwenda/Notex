using MediatR;
using Notex.Core.Authorization;
using Notex.Core.Configuration;
using Notex.Messages.Comments.Commands;

namespace Notex.Core.Aggregates.Comments.CommandHandlers;

public class EditCommentCommandHandler : ICommandHandler<EditCommentCommand>
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IResourceAuthorizationService _resourceAuthorizationService;

    public EditCommentCommandHandler(
        IAggregateRepository aggregateRepository,
        IResourceAuthorizationService resourceAuthorizationService)
    {
        _aggregateRepository = aggregateRepository;
        _resourceAuthorizationService = resourceAuthorizationService;
    }

    public async Task<Unit> Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _aggregateRepository.LoadAsync<Comment>(request.CommentId);
        
        await _resourceAuthorizationService.CheckAsync(comment, CommonOperations.Update);

        comment.Edit(request.Text);

        await _aggregateRepository.SaveAsync(comment);

        return Unit.Value;
    }
}