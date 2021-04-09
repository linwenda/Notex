using System;
using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.Services.Albums.Application.Commands.ChangeVisibility
{
    public class ChangeVisibilityCommand : CommandBase
    {
        public ChangeVisibilityCommand(Guid albumId, string visibility)
        {
            AlbumId = albumId;
            Visibility = visibility;
        }

        public Guid AlbumId { get; }
        public string Visibility { get; }
    }
}