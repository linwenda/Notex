using MediatR;
using Notex.Core.Domain.Notes;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class DeleteNoteCommandHandler : ICommandHandler<DeleteNoteCommand>
{
    private readonly IEventSourcedRepository<Note> _noteRepository;

    public DeleteNoteCommandHandler(IEventSourcedRepository<Note> noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(request.NoteId, cancellationToken);

        note.Delete();

        await _noteRepository.SaveAsync(note, cancellationToken);

        return Unit.Value;
    }
}