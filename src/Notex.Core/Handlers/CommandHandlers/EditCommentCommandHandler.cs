using MediatR;
using Notex.Core.Domain.Comments;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Identity;
using Notex.Messages.Comments.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class EditCommentCommandHandler : ICommandHandler<EditCommentCommand>
{
    private readonly IEventSourcedRepository<Comment> _commentRepository;
    private readonly IResourceAuthorizationService _resourceAuthorizationService;

    public EditCommentCommandHandler(
        IEventSourcedRepository<Comment> commentRepository,
            IResourceAuthorizationService resourceAuthorizationService)
    {
        _commentRepository = commentRepository;
        _resourceAuthorizationService = resourceAuthorizationService;
    }

    public async Task<Unit> Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetAsync(request.CommentId, cancellationToken);
        
        await _resourceAuthorizationService.CheckAsync(comment, CommonOperations.Update);

        comment.Edit(request.Text);

        await _commentRepository.SaveAsync(comment, cancellationToken);

        return Unit.Value;
    }
}