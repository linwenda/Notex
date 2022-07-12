namespace Notex.Messages.Notes.Queries;

public class GetNoteHistoryQuery : IQuery<NoteHistoryDto>
{
    public Guid NoteHistoryId { get; }

    public GetNoteHistoryQuery(Guid noteHistoryId)
    {
        NoteHistoryId = noteHistoryId;
    }
}