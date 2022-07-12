namespace Notex.Messages.Notes.Commands;

public class UpdateNoteTagsCommand : ICommand
{
    public UpdateNoteTagsCommand(Guid noteId, ICollection<string> tags)
    {
        NoteId = noteId;
        Tags = tags;
    }

    public Guid NoteId { get;  }
    public ICollection<string> Tags { get;  }
}