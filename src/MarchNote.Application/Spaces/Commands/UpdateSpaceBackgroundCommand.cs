using System;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.Spaces.Commands
{
    public class UpdateSpaceBackgroundCommand : ICommand<Unit>
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