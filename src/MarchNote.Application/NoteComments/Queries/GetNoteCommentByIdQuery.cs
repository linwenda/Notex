using System;
using MarchNote.Application.Configuration.Queries;

namespace MarchNote.Application.NoteComments.Queries
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