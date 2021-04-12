using System;
using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.Services.Albums.Application.Commands.DeletePictureComment
{
    public class DeletePictureCommentCommand : CommandBase
    {
        public Guid PictureCommentId { get; set; }
    }
}