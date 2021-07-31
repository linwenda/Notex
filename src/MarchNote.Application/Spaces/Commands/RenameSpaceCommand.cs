using System;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Commands
{
    public class RenameSpaceCommand : ICommand<MarchNoteResponse>
    {
        public Guid SpaceId { get; }
        public string Name { get; }

        public RenameSpaceCommand(Guid spaceId,string name)
        {
            SpaceId = spaceId;
            Name = name;
        }
    }
}