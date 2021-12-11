namespace SmartNote.Core.Domain.NoteCooperations.Exceptions
{
    public class InvalidCooperationStatusException : BusinessException
    {
        public InvalidCooperationStatusException() : base(DomainErrorCodes.InvalidCooperationStatus,
            "Invalid status")
        {
        }
    }
}