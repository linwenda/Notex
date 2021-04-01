using System;
using Funzone.BuildingBlocks.Application.Queries;

namespace Funzone.Services.Albums.Application.Queries.GetAlbum
{
    public class GetAlbumByIdQuery : IQuery<AlbumDto>
    {
        public GetAlbumByIdQuery(Guid albumId)
        {
            AlbumId = albumId;
        }

        public Guid AlbumId { get; }
    }
}