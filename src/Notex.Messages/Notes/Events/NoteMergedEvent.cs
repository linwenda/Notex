namespace Notex.Messages.Notes.Events;

public class NoteMergedEvent : VersionedEvent
{
    public Guid SourceNoteId { get; }
    public string Title { get; }
    public string Content { get; }

    public NoteMergedEvent(Guid sourcedId, int version, Guid sourceNoteId, string title, string content) :
        base(sourcedId, version)
    {
        SourceNoteId = sourceNoteId;
        Title = title;
        Content = content;
    }
}