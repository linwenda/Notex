using System;

namespace Funzone.Services.Albums.Application.Queries.GetAlbum
{
    public class AlbumDto
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Visibility { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}