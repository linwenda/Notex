using System;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.NoteComments.Queries
{
    public class GetNoteCommentByIdQuery : IQuery<MarchNoteResponse<NoteCommentDto>>
    {
        public Guid CommentId { get; }

        public GetNoteCommentByIdQuery(Guid commentId)
        {
            CommentId = commentId;
        }
    }
}