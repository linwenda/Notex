using System;
using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.Services.Albums.Application.Commands.CreateAlbum
{
    public class CreateAlbumCommand : CommandBase<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}