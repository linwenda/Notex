using System;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.NoteMergeRequests.Commands
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