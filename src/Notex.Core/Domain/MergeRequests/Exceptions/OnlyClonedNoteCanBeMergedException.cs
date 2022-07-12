using Notex.Core.Exceptions;

namespace Notex.Core.Domain.MergeRequests.Exceptions;

public class OnlyClonedNoteCanBeMergedException : BusinessException
{
    public OnlyClonedNoteCanBeMergedException() : base("Only cloned note can be merged")
    {
    }
}