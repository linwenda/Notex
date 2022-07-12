using Notex.Core.Exceptions;

namespace Notex.Core.Domain.MergeRequests.Exceptions;

public class OnlyOpenMergeRequestCanBeUpdatedException : BusinessException
{
    public OnlyOpenMergeRequestCanBeUpdatedException() : base("Only open merge-request can be updated")
    {
    }
}