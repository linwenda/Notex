using MediatR;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.NoteMergeRequests.Commands
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