namespace SmartNote.Core.Domain.NoteMergeRequests.Exceptions
{
    public class OnlyNoteAuthorCanBeMergedException : BusinessException
    {
        public OnlyNoteAuthorCanBeMergedException() : base(DomainErrorCodes.OnlyNoteAuthorCanBeMerged,
            "Only note author can be merged")
        {
        }
    }
}