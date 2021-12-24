using SmartNote.Application.Configuration.Queries;
using SmartNote.Domain.NoteMergeRequests;

namespace SmartNote.Application.NoteMergeRequests.Queries
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