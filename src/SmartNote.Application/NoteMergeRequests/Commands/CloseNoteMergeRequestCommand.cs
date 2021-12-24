using MediatR;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.NoteMergeRequests.Commands
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