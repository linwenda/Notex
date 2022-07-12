using Notex.Core.Exceptions;
using Notex.Messages.MergeRequests;

namespace Notex.Core.Domain.MergeRequests.Exceptions;

public class MergeRequestStatusChangeException : BusinessException
{
    public MergeRequestStatusChangeException(MergeRequestStatus currentStatus,
        MergeRequestStatus statusToChange) : base(
        $"Is not possible to change the merge-request status from {currentStatus} to {statusToChange}")
    {
    }
}