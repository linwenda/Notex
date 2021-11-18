using System;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.NoteMergeRequests.Commands
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