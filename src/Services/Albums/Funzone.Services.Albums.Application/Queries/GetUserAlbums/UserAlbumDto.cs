using System;

namespace Funzone.Services.Albums.Application.Queries.GetUserAlbums
{
    public class UserAlbumDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Visibility { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}