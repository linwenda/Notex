using System;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Commands
{
    public class MoveSpaceCommand : ICommand<MarchNoteResponse>
    {
        public Guid SpaceId { get; }
        public Guid DestSpaceId { get; }

        public MoveSpaceCommand(Guid spaceFolderId, Guid destSpaceFolderId)
        {
            SpaceId = spaceFolderId;
            DestSpaceId = destSpaceFolderId;
        }
    }
}