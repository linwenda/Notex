using Notex.Core.Exceptions;

namespace Notex.Core.Domain.MergeRequests.Exceptions;

public class OnlyClonedNoteCanCreateMergeRequestException : BusinessException
{
    public OnlyClonedNoteCanCreateMergeRequestException() : base("Only cloned note can create merge-request")
    {
    }
}