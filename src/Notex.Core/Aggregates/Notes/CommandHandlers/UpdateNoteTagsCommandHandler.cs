using MediatR;
using Notex.Core.Configuration;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Aggregates.Notes.CommandHandlers;

public class UpdateNoteTagsCommandHandler : ICommandHandler<UpdateNoteTagsCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public UpdateNoteTagsCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Unit> Handle(UpdateNoteTagsCommand request, CancellationToken cancellationToken)
    {
        var note = await _aggregateRepository.LoadAsync<Note>(request.NoteId);

        note.UpdateTags(request.Tags);

        await _aggregateRepository.SaveAsync(note);

        return Unit.Value;
    }
}