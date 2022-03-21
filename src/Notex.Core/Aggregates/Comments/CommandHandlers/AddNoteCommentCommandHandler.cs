using Notex.Core.Aggregates.Notes;
using Notex.Core.Configuration;
using Notex.Messages.Comments.Commands;

namespace Notex.Core.Aggregates.Comments.CommandHandlers;

public class AddNoteCommentCommandHandler : ICommandHandler<AddNoteCommentCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAggregateRepository _aggregateRepository;

    public AddNoteCommentCommandHandler(ICurrentUser currentUser, IAggregateRepository aggregateRepository)
    {
        _currentUser = currentUser;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Guid> Handle(AddNoteCommentCommand request, CancellationToken cancellationToken)
    {
        var note = await _aggregateRepository.LoadAsync<Note>(request.NoteId);

        var comment = note.AddComment(_currentUser.Id, request.Text);

        await _aggregateRepository.SaveAsync(comment);

        return comment.Id;
    }
}