using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Notes.Exceptions
{
    public class InvalidNoteStatusException : BusinessNewException
    {
        public InvalidNoteStatusException(string message = "Invalid note status") : base(
            DomainErrorCodes.InvalidNoteStatus, message)
        {
        }
    }
}