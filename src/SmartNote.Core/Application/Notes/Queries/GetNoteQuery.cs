namespace SmartNote.Core.Application.Notes.Queries
{
    public class GetNoteQuery : IQuery<NoteDto>
    {
        public Guid NoteId { get; }

        public GetNoteQuery(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}