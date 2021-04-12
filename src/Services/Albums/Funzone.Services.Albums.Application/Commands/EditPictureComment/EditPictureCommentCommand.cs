using System;
using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.Services.Albums.Application.Commands.EditPictureComment
{
    public class EditPictureCommentCommand : CommandBase
    {
        public Guid PictureCommentId { get; set; }
        public string Comment { get; set; }
    }
}