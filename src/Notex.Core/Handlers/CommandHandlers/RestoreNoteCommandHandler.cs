using MediatR;
using Notex.Core.Domain.Notes;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Identity;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class RestoreNoteCommandHandler : ICommandHandler<RestoreNoteCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly INoteService _noteService;
    private readonly IEventSourcedRepository<Note> _eventSourcedRepository;

    public RestoreNoteCommandHandler(
        ICurrentUser currentUser,
        INoteService noteService,
        IEventSourcedRepository<Note> eventSourcedRepository)
    {
        _currentUser = currentUser;
        _noteService = noteService;
        _eventSourcedRepository = eventSourcedRepository;
    }

    public async Task<Unit> Handle(RestoreNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _eventSourcedRepository.GetAsync(request.NoteId, cancellationToken);

        await _noteService.RestoreNoteAsync(note, request.NoteHistoryId, _currentUser.Id);

        await _eventSourcedRepository.SaveAsync(note, cancellationToken);

        return Unit.Value;
    }
}