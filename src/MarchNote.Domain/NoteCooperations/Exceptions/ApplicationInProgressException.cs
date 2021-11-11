using MarchNote.Domain.Shared;

namespace MarchNote.Domain.NoteCooperations.Exceptions
{
    public class ApplicationInProgressException : BusinessNewException
    {
        public ApplicationInProgressException() : base(DomainErrorCodes.CooperationApplicationInProgress,
            "Cooperation application in progress")
        {
        }
    }
}