using System;
using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.Services.Albums.Application.Commands.AddPictureComment
{
    public class AddPictureCommentCommand : CommandBase
    {
        public Guid PictureId { get; set; }
        public string Comment { get; set; }
    }
}