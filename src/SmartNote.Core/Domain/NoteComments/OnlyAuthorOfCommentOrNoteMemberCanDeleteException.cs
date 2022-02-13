namespace SmartNote.Core.Domain.NoteComments
{
    public class OnlyAuthorOfCommentOrNoteMemberCanDeleteException : BusinessException
    {
        public OnlyAuthorOfCommentOrNoteMemberCanDeleteException() : base(
            DomainErrorCodes.OnlyAuthorOfCommentOrNoteMemberCanDelete,
            "Only author of comment or note member can delete")
        {
        }
    }
}