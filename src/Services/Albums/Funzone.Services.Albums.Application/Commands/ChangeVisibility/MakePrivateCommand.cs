using System;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.Services.Albums.Domain.Albums;

namespace Funzone.Services.Albums.Application.Commands.ChangeVisibility
{
    public class MakePrivateCommand : CommandBase
    {
        public Guid AlbumId { get; }

        public MakePrivateCommand(Guid albumId)
        {
            AlbumId = albumId;
        }
    }
}