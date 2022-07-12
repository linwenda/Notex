using Notex.Core.Domain.Notes;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Domain.Spaces;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class CreateNoteCommandHandler : ICommandHandler<CreateNoteCommand, Guid>
{
    private readonly IEventSourcedRepository<Space> _spaceRepository;
    private readonly IEventSourcedRepository<Note> _noteRepository;

    public CreateNoteCommandHandler(
        IEventSourcedRepository<Space> spaceRepository,
        IEventSourcedRepository<Note> noteRepository)
    {
        _spaceRepository = spaceRepository;
        _noteRepository = noteRepository;
    }

    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var space = await _spaceRepository.GetAsync(request.SpaceId, cancellationToken);

        var note = space.CreateNote(
            request.Title,
            request.Content,
            request.Status);

        await _noteRepository.SaveAsync(note, cancellationToken);

        return note.Id;
    }
}