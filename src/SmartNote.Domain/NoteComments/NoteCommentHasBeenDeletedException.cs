using SmartNote.Core.Ddd;

namespace SmartNote.Domain.NoteComments
{
    public class NoteCommentHasBeenDeletedException : BusinessException
    {
        public NoteCommentHasBeenDeletedException() : base(
            DomainErrorCodes.NoteCommentHasBeenDeleted, "Comment has been deleted")
        {
        }
    }
}