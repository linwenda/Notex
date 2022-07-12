using Notex.Core.Exceptions;

namespace Notex.Core.Domain.MergeRequests.Exceptions;

public class MergeRequestHasBeenExistsException : BusinessException
{
    public MergeRequestHasBeenExistsException() : base("Merge-request has been exists")
    {
    }
}