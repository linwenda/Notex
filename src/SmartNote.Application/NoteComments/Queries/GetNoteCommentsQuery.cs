using SmartNote.Application.Configuration.Queries;

namespace SmartNote.Application.NoteComments.Queries
{
    public class GetNoteCommentsQuery : IQuery<IEnumerable<NoteCommentDto>>
    {
        public Guid NoteId { get; }

        public GetNoteCommentsQuery(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}