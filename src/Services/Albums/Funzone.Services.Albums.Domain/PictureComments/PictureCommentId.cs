using System;
using Funzone.BuildingBlocks.Domain;

namespace Funzone.Services.Albums.Domain.PictureComments
{
    public class PictureCommentId : TypedIdValueBase
    {
        public PictureCommentId(Guid value) : base(value)
        {
        }
    }
}