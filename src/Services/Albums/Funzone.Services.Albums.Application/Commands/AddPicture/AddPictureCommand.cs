using System;
using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.Services.Albums.Application.Commands.AddPicture
{
    public class AddPictureCommand : CommandBase
    {
        public Guid AlbumId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string ThumbnailLink { get; set; }
        public string Description { get; set; }
    }
}