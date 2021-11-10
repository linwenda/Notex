using System;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.Spaces.Commands
{
    public class DeleteSpaceCommand : ICommand<Unit>
    {
        public Guid SpaceId { get; }

        public DeleteSpaceCommand(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}