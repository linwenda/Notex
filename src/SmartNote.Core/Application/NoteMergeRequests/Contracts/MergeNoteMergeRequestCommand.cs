using MediatR;

namespace SmartNote.Core.Application.NoteMergeRequests.Contracts
{
    public class MergeNoteMergeRequestCommand : ICommand<Unit>
    {
        public Guid NoteMergeRequestId { get; }

        public MergeNoteMergeRequestCommand(Guid noteMergeRequestId)
        {
            NoteMergeRequestId = noteMergeRequestId;
        }
    }
}