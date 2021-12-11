namespace SmartNote.Core.Application.NoteComments.Contracts
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