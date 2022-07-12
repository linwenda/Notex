using MediatR;
using Notex.Core.Domain.Notes;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class PublishNoteCommandHandler : ICommandHandler<PublishNoteCommand>
{
    private readonly IEventSourcedRepository<Note> _noteRepository;

    public PublishNoteCommandHandler(IEventSourcedRepository<Note> noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Unit> Handle(PublishNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(request.NoteId, cancellationToken);

        note.Publish();

        await _noteRepository.SaveAsync(note, cancellationToken);

        return Unit.Value;
    }
}