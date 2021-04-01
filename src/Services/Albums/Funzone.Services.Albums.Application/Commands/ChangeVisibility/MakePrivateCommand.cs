using Funzone.BuildingBlocks.Application.Commands;
using Funzone.Services.Albums.Domain.Albums;

namespace Funzone.Services.Albums.Application.Commands.ChangeVisibility
{
    public class MakePrivateCommand : CommandBase
    {
        public AlbumId AlbumId { get; }

        public MakePrivateCommand(AlbumId albumId)
        {
            AlbumId = albumId;
        }
    }
}