using System;

namespace Funzone.Application.Queries.Zones
{
    public class ZoneDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string AvatarUrl { get; set; }
    }
}