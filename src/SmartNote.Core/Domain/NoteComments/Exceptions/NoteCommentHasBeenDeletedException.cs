namespace SmartNote.Core.Domain.NoteComments.Exceptions
{
    public class NoteCommentHasBeenDeletedException : BusinessException
    {
        public NoteCommentHasBeenDeletedException() : base(
            DomainErrorCodes.NoteCommentHasBeenDeleted, "Comment has been deleted")
        {
        }
    }
}