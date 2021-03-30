using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.Services.Albums.Application.Commands.CreateAlbum
{
    public class CreateAlbumCommand : CommandBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}