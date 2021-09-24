using System;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Spaces.Commands
{
    public class UpdateSpaceBackgroundCommand : ICommand<MarchNoteResponse>
    {
        public Guid SpaceId { get; }
        public Guid BackgroundImageId { get; }

        public UpdateSpaceBackgroundCommand(Guid spaceId, Guid backgroundImageId)
        {
            SpaceId = spaceId;
            BackgroundImageId = backgroundImageId;
        }
    }
}