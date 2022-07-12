namespace Notex.Messages.Notes.Queries;

public class GetNoteHistoriesQuery : IQuery<IEnumerable<NoteHistoryDto>>
{
    public Guid NoteId { get; }

    public GetNoteHistoriesQuery(Guid noteId)
    {
        NoteId = noteId;
    }
}