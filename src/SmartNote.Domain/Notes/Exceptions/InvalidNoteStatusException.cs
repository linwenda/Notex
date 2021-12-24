namespace SmartNote.Domain.Notes.Exceptions
{
    public class InvalidNoteStatusException : BusinessException
    {
        public InvalidNoteStatusException(string message = "Invalid note status") : base(
            DomainErrorCodes.InvalidNoteStatus, message)
        {
        }
    }
}