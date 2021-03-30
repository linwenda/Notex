using System;
using Funzone.BuildingBlocks.Domain;

namespace Funzone.Services.Albums.Domain.Pictures
{
    public class PictureId : TypedIdValueBase
    {
        public PictureId(Guid value) : base(value)
        {
        }
    }
}