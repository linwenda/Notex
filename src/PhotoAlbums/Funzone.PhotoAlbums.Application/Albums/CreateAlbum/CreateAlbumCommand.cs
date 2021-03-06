using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.PhotoAlbums.Application.Albums.CreateAlbum
{
    public class CreateAlbumCommand : CommandBase
    {
        public string Name { get; set; }
    }
}