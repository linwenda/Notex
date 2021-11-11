using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Notes.Exceptions
{
    public class UserHasBeenJoinedThisNoteCooperationException : BusinessNewException
    {
        public UserHasBeenJoinedThisNoteCooperationException() : base(
            DomainErrorCodes.UserHasBeenJoinedThisNoteCooperation,
            "You have joined the note cooperation")
        {
        }
    }
}