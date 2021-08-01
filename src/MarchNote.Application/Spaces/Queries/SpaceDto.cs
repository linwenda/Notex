using System;
using MarchNote.Domain.Spaces;

namespace MarchNote.Application.Spaces.Queries
{
    public class SpaceDto
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public SpaceType Type { get; set; }
    }
}