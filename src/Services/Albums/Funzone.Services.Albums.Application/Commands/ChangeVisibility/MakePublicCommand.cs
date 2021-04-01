using System;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.Services.Albums.Domain.Albums;

namespace Funzone.Services.Albums.Application.Commands.ChangeVisibility
{
    public class MakePublicCommand : CommandBase
    {
        public MakePublicCommand(Guid albumId)
        {
            AlbumId = albumId;
        }

        public Guid AlbumId { get; }
    }
}