using MediatR;
using Notex.Core.Configuration;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Aggregates.Notes.CommandHandlers;

public class MergeNoteCommandHandler : ICommandHandler<MergeNoteCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public MergeNoteCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Unit> Handle(MergeNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _aggregateRepository.LoadAsync<Note>(request.NoteId);

        note.Merge(request.SourceNoteId, request.Title, request.Content);

        await _aggregateRepository.SaveAsync(note);

        return Unit.Value;
    }
}