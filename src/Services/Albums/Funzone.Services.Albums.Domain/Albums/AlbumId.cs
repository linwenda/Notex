using System;
using Funzone.BuildingBlocks.Domain;

namespace Funzone.Services.Albums.Domain.Albums
{
    public class AlbumId : TypedIdValueBase
    {
        public AlbumId(Guid value) : base(value)
        {
        }
    }
}