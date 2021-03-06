using Funzone.BuildingBlocks.Domain;
using System;

namespace Funzone.PhotoAlbums.Domain.Albums
{
    public class AlbumId : TypedIdValueBase
    {
        public AlbumId(Guid value) : base(value)
        {
        }
    }
}