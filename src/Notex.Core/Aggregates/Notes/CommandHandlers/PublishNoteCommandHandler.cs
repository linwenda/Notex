using MediatR;
using Notex.Core.Configuration;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Aggregates.Notes.CommandHandlers;

public class PublishNoteCommandHandler : ICommandHandler<PublishNoteCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public PublishNoteCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Unit> Handle(PublishNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _aggregateRepository.LoadAsync<Note>(request.NoteId);

        note.Publish();

        await _aggregateRepository.SaveAsync(note);

        return Unit.Value;
    }
}