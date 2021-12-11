namespace SmartNote.Core.Domain.NoteMergeRequests.Exceptions
{
    public class OnlyNoteAuthorOrCreatorCanBeClosedException : BusinessException
    {
        public OnlyNoteAuthorOrCreatorCanBeClosedException() : base(DomainErrorCodes.OnlyNoteAuthorCanBeMerged,
            "Only note author or creator can ben closed")
        {
        }
    }
}