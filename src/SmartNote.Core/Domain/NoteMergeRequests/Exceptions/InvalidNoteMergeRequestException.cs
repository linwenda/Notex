namespace SmartNote.Core.Domain.NoteMergeRequests.Exceptions
{
    public class InvalidNoteMergeRequestException : BusinessException
    {
        public InvalidNoteMergeRequestException() : base(DomainErrorCodes.InvalidNoteMergeRequest,
            "Invalid note merge request")
        {
        }
    }
}