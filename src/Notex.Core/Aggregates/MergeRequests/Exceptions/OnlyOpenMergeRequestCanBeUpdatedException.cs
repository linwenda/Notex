using Notex.Core.Exceptions;

namespace Notex.Core.Aggregates.MergeRequests.Exceptions;

public class OnlyOpenMergeRequestCanBeUpdatedException : BusinessException
{
    public OnlyOpenMergeRequestCanBeUpdatedException() : base("Only open merge-request can be updated")
    {
    }
}