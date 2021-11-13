using System.Collections.Generic;
using MarchNote.Application.Configuration.Queries;
using MarchNote.Domain.NoteMergeRequests;

namespace MarchNote.Application.NoteMergeRequests.Queries
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