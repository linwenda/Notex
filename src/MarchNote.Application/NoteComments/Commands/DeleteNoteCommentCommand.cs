using System;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.NoteComments.Commands
{
    public class DeleteNoteCommentCommand : ICommand<MarchNoteResponse>
    {
        public Guid CommentId { get; }

        public DeleteNoteCommentCommand(Guid commentId)
        {
            CommentId = commentId;
        }
    }
}