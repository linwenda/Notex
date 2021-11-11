using MarchNote.Domain.Shared;

namespace MarchNote.Domain.NoteCooperations.Exceptions
{
    public class InvalidCooperationStatusException : BusinessNewException
    {
        public InvalidCooperationStatusException() : base(DomainErrorCodes.InvalidCooperationStatus,
            "Invalid status")
        {
        }
    }
}