using MediatR;
using Notex.Core.Domain.Notes;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Identity;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class EditNoteCommandHandler : ICommandHandler<EditNoteCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IEventSourcedRepository<Note> _noteRepository;

    public EditNoteCommandHandler(ICurrentUser currentUser,IEventSourcedRepository<Note> noteRepository)
    {
        _currentUser = currentUser;
        _noteRepository = noteRepository;
    }

    public async Task<Unit> Handle(EditNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(request.NoteId, cancellationToken);

        note.Edit(_currentUser.Id, request.Title, request.Content, request.Comment);

        await _noteRepository.SaveAsync(note, cancellationToken);

        return Unit.Value;
    }
}