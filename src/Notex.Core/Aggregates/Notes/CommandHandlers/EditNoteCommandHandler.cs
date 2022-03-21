using MediatR;
using Notex.Core.Configuration;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Aggregates.Notes.CommandHandlers;

public class EditNoteCommandHandler : ICommandHandler<EditNoteCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAggregateRepository _aggregateRepository;

    public EditNoteCommandHandler(ICurrentUser currentUser, IAggregateRepository aggregateRepository)
    {
        _currentUser = currentUser;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Unit> Handle(EditNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _aggregateRepository.LoadAsync<Note>(request.NoteId);

        note.Edit(_currentUser.Id, request.Title, request.Content, request.Comment);

        await _aggregateRepository.SaveAsync(note);

        return Unit.Value;
    }
}