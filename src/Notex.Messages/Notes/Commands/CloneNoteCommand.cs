using System;

namespace Notex.Messages.Notes.Commands;

public class CloneNoteCommand : ICommand<Guid>
{
    public Guid NoteId { get; }
    public Guid SpaceId { get; }

    public CloneNoteCommand(Guid noteId, Guid spaceId)
    {
        NoteId = noteId;
        SpaceId = spaceId;
    }
}