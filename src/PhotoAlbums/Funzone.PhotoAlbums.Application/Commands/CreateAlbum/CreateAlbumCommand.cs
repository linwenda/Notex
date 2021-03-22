using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.PhotoAlbums.Application.Commands.CreateAlbum
{
    public class CreateAlbumCommand : CommandBase
    {
        public string Name { get; set; }
    }
}