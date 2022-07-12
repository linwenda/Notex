namespace Notex.Messages.Notes.Events;

public class NoteEditedEvent : VersionedEvent
{
    public Guid UserId { get; }
    public string Title { get; }
    public string Content { get; }
    public NoteStatus Status { get; }
    public string Comment { get; }

    public NoteEditedEvent(Guid sourcedId, int version, Guid userId, string title, string content,
        NoteStatus status, string comment) :
        base(sourcedId, version)
    {
        UserId = userId;
        Title = title;
        Content = content;
        Status = status;
        Comment = comment;
    }
}