using System;

namespace MarchNote.Application.Spaces.Queries
{
    public class SpaceDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
    }
}