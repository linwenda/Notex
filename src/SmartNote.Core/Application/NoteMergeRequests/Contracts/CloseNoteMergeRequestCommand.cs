using MediatR;

namespace SmartNote.Core.Application.NoteMergeRequests.Contracts
{
    public class CloseNoteMergeRequestCommand : ICommand<Unit>
    {
        public Guid NoteMergeRequestId { get; }

        public CloseNoteMergeRequestCommand(Guid noteMergeRequestId)
        {
            NoteMergeRequestId = noteMergeRequestId;
        }
    }
}