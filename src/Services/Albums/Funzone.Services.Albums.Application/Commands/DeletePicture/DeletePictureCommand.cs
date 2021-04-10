using System;
using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.Services.Albums.Application.Commands.DeletePicture
{
    public class DeletePictureCommand : CommandBase
    {
        public Guid PictureId { get; set; }
    }
}