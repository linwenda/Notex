using MediatR;
using Notex.Core.Domain.Notes;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class UpdateNoteTagsCommandHandler : ICommandHandler<UpdateNoteTagsCommand>
{
    private readonly IEventSourcedRepository<Note> _noteRepository;

    public UpdateNoteTagsCommandHandler(IEventSourcedRepository<Note> noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Unit> Handle(UpdateNoteTagsCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(request.NoteId, cancellationToken);

        note.UpdateTags(request.Tags);

        await _noteRepository.SaveAsync(note, cancellationToken);

        return Unit.Value;
    }
}