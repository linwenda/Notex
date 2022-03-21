using MediatR;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Aggregates.Notes.ReadModels;
using Notex.Core.Configuration;
using Notex.Core.Exceptions;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Aggregates.Notes.CommandHandlers;

public class RestoreNoteCommandHandler : ICommandHandler<RestoreNoteCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IReadModelRepository _readModelRepository;
    private readonly IAggregateRepository _aggregateRepository;

    public RestoreNoteCommandHandler(
        ICurrentUser currentUser,
        IReadModelRepository readModelRepository,
        IAggregateRepository aggregateRepository)
    {
        _currentUser = currentUser;
        _readModelRepository = readModelRepository;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Unit> Handle(RestoreNoteCommand request, CancellationToken cancellationToken)
    {
        var noteHistory = await _readModelRepository.Query<NoteHistory>()
            .FirstOrDefaultAsync(h => h.Id == request.NoteHistoryId && h.NoteId == request.NoteId, cancellationToken);

        if (noteHistory == null)
        {
            throw new EntityNotFoundException(typeof(NoteHistory), request.NoteHistoryId);
        }

        var note = await _aggregateRepository.LoadAsync<Note>(request.NoteId);

        note.Restore(_currentUser.Id, noteHistory.Title, noteHistory.Content, noteHistory.Version);

        await _aggregateRepository.SaveAsync(note);

        return Unit.Value;
    }
}