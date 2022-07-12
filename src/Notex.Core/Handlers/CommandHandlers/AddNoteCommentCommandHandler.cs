using Notex.Core.Domain.Comments;
using Notex.Core.Domain.Notes;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Identity;
using Notex.Messages.Comments.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class AddNoteCommentCommandHandler : ICommandHandler<AddNoteCommentCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly IEventSourcedRepository<Note> _noteRepository;
    private readonly IEventSourcedRepository<Comment> _commentRepository;

    public AddNoteCommentCommandHandler(ICurrentUser currentUser, 
        IEventSourcedRepository<Note> noteRepository,
        IEventSourcedRepository<Comment> commentRepository)
    {
        _currentUser = currentUser;
        _noteRepository = noteRepository;
        _commentRepository = commentRepository;
    }

    public async Task<Guid> Handle(AddNoteCommentCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(request.NoteId, cancellationToken);

        var comment = note.AddComment(_currentUser.Id, request.Text);

        await _commentRepository.SaveAsync(comment, cancellationToken);

        return comment.Id;
    }
}