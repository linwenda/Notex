using Notex.Core.Aggregates.Spaces;
using Notex.Core.Configuration;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Aggregates.Notes.CommandHandlers;

public class CreateNoteCommandHandler : ICommandHandler<CreateNoteCommand, Guid>
{
    private readonly IAggregateRepository _aggregateRepository;

    public CreateNoteCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    [UnitOfWork]
    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var space = await _aggregateRepository.LoadAsync<Space>(request.SpaceId);

        var note = space.CreateNote(
            request.Title,
            request.Content,
            request.Status);

        await _aggregateRepository.SaveAsync(note);

        return note.Id;
    }
}