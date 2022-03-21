using Notex.Core.Configuration;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Aggregates.Notes.CommandHandlers;

public class CloneNoteCommandHandler : ICommandHandler<CloneNoteCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAggregateRepository _aggregateRepository;

    public CloneNoteCommandHandler(
        ICurrentUser currentUser,
        IAggregateRepository aggregateRepository)
    {
        _currentUser = currentUser;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Guid> Handle(CloneNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _aggregateRepository.LoadAsync<Note>(request.NoteId);

        var cloneNote = note.Clone(_currentUser.Id, request.SpaceId);

        await _aggregateRepository.SaveAsync(cloneNote);

        return cloneNote.Id;
    }
}