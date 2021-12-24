namespace SmartNote.Domain.Notes.Exceptions
{
    public class UserHasBeenJoinedThisNoteCooperationException : BusinessException
    {
        public UserHasBeenJoinedThisNoteCooperationException() : base(
            DomainErrorCodes.UserHasBeenJoinedThisNoteCooperation,
            "You have joined the note cooperation")
        {
        }
    }
}