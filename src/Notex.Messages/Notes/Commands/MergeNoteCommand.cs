using System;

namespace Notex.Messages.Notes.Commands;

public class MergeNoteCommand : IInternalCommand
{
    public Guid NoteId { get; }
    public Guid SourceNoteId { get; }
    public string Title { get; }
    public string Content { get; }

    public MergeNoteCommand(Guid noteId, Guid sourceNoteId, string title, string content)
    {
        NoteId = noteId;
        SourceNoteId = sourceNoteId;
        Title = title;
        Content = content;
    }
}