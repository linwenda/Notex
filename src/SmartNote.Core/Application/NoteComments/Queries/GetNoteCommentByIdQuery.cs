namespace SmartNote.Core.Application.NoteComments.Queries
{
    public class GetNoteCommentByIdQuery : IQuery<NoteCommentDto>
    {
        public Guid CommentId { get; }

        public GetNoteCommentByIdQuery(Guid commentId)
        {
            CommentId = commentId;
        }
    }
}