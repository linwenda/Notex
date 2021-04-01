using Funzone.BuildingBlocks.Application.Commands;
using Funzone.Services.Albums.Domain.Albums;

namespace Funzone.Services.Albums.Application.Commands.ChangeVisibility
{
    public class MakePublicCommand : CommandBase
    {
        public MakePublicCommand(AlbumId albumId)
        {
            AlbumId = albumId;
        }

        public AlbumId AlbumId { get; }
    }
}