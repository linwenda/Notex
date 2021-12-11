using MediatR;

namespace SmartNote.Core.Application.NoteComments.Contracts
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