using SmartNote.Core.Domain.NoteMergeRequests;

namespace SmartNote.Core.Application.NoteMergeRequests.Queries
{
    public class GetNoteMergeRequestQuery : IQuery<IEnumerable<NoteMergeRequestDto>>
    {
        public NoteMergeRequestStatus Status { get; }

        public GetNoteMergeRequestQuery(NoteMergeRequestStatus status)
        {
            Status = status;
        }
    }
}