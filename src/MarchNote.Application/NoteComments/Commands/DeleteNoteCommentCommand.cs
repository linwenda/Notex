using System;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.NoteComments.Commands
{
    public class DeleteNoteCommentCommand : ICommand<Unit>
    {
        public Guid CommentId { get; }

        public DeleteNoteCommentCommand(Guid commentId)
        {
            CommentId = commentId;
        }
    }
}