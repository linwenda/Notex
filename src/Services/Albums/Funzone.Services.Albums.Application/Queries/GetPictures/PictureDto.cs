using System;

namespace Funzone.Services.Albums.Application.Queries.GetPictures
{
    public class PictureDto
    {
        public Guid AlbumId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string ThumbnailLink { get; set; }
    }
}