﻿using SmartNote.Application.Configuration.Queries;

namespace SmartNote.Application.NoteComments.Queries
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