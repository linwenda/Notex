using System;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Commands
{
    public class DeleteSpaceCommand : ICommand<MarchNoteResponse>
    {
        public Guid SpaceId { get; }

        public DeleteSpaceCommand(Guid spaceId)
        {
            SpaceId = spaceId;
        }
    }
}