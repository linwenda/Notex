using System;
using System.Collections.Generic;
using Funzone.BuildingBlocks.Application.Queries;

namespace Funzone.Services.Albums.Application.Queries.GetPictures
{
    public class GetPicturesByAlbumIdQuery : IQuery<List<PictureDto>>
    {
        public Guid AlbumId { get; }

        public GetPicturesByAlbumIdQuery(Guid albumId)
        {
            AlbumId = albumId;
        }
    }
}