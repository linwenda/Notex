using System;
using Funzone.BuildingBlocks.Application.Queries;

namespace Funzone.Services.Albums.Application.Queries.GetPictures
{
    public class GetPictureByIdQuery : IQuery<PictureDto>
    {
        public GetPictureByIdQuery(Guid pictureId)
        {
            PictureId = pictureId;
        }

        public Guid PictureId { get; }
    }
}