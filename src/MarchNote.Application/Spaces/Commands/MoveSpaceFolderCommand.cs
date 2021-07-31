using System;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Commands
{
    public class MoveSpaceFolderCommand : ICommand<MarchNoteResponse>
    {
        public Guid SpaceFolderId { get; }
        public Guid? DestSpaceFolderId { get; }

        public MoveSpaceFolderCommand(Guid spaceFolderId, Guid? destSpaceFolderId)
        {
            SpaceFolderId = spaceFolderId;
            DestSpaceFolderId = destSpaceFolderId;
        }
    }
}