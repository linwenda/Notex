using MediatR;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.NoteComments.Commands
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