using MediatR;
using Notex.Core.Configuration;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Aggregates.Notes.CommandHandlers;

public class DeleteNoteCommandHandler : ICommandHandler<DeleteNoteCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public DeleteNoteCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _aggregateRepository.LoadAsync<Note>(request.NoteId);

        note.Delete();

        await _aggregateRepository.SaveAsync(note);

        return Unit.Value;
    }
}