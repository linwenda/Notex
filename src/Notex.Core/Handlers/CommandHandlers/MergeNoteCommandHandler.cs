using MediatR;
using Notex.Core.Domain.Notes;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Handlers.CommandHandlers;

public class MergeNoteCommandHandler : ICommandHandler<MergeNoteCommand>
{
    private readonly IEventSourcedRepository<Note> _noteRepository;

    public MergeNoteCommandHandler(IEventSourcedRepository<Note> noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Unit> Handle(MergeNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(request.NoteId, cancellationToken);

        note.Merge(request.SourceNoteId, request.Title, request.Content);

        await _noteRepository.SaveAsync(note, cancellationToken);

        return Unit.Value;
    }
}