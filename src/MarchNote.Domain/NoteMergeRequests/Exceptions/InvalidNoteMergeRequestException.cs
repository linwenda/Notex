using MarchNote.Domain.Shared;

namespace MarchNote.Domain.NoteMergeRequests.Exceptions
{
    public class InvalidNoteMergeRequestException : BusinessException
    {
        public InvalidNoteMergeRequestException() : base(DomainErrorCodes.InvalidNoteMergeRequest,
            "Invalid note merge request")
        {
        }
    }
}