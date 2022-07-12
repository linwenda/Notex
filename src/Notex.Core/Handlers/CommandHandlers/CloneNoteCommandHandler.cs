using Notex.Core.Domain.Notes;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Identity;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class CloneNoteCommandHandler : ICommandHandler<CloneNoteCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly IEventSourcedRepository<Note> _noteRepository;

    public CloneNoteCommandHandler(
        ICurrentUser currentUser,
        IEventSourcedRepository<Note> noteRepository)
    {
        _currentUser = currentUser;
        _noteRepository = noteRepository;
    }

    public async Task<Guid> Handle(CloneNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(request.NoteId, cancellationToken);

        var cloneNote = note.Clone(_currentUser.Id, request.SpaceId);

        await _noteRepository.SaveAsync(cloneNote, cancellationToken);

        return cloneNote.Id;
    }
}