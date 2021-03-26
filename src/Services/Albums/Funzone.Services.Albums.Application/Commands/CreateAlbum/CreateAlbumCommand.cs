using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.Services.Albums.Application.Commands.CreateAlbum
{
    public class CreateAlbumCommand : CommandBase
    {
        public string Name { get; set; }
    }
}