using MediatR;

namespace SmartNote.Core.Application.NoteMergeRequests.Commands
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