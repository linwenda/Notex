using SmartNote.Application.Configuration.Queries;

namespace SmartNote.Application.Notes.Queries
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