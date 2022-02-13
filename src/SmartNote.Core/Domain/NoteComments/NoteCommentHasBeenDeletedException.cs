namespace SmartNote.Core.Domain.NoteComments
{
    public class NoteCommentHasBeenDeletedException : BusinessException
    {
        public NoteCommentHasBeenDeletedException() : base(
            DomainErrorCodes.NoteCommentHasBeenDeleted, "Comment has been deleted")
        {
        }
    }
}