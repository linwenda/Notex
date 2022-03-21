using System;

namespace Notex.Messages.Notes.Commands;

public class RestoreNoteCommand : ICommand
{
    public Guid NoteId { get; }
    public Guid NoteHistoryId { get; }

    public RestoreNoteCommand(Guid noteId, Guid noteHistoryId)
    {
        NoteId = noteId;
        NoteHistoryId = noteHistoryId;
    }
}