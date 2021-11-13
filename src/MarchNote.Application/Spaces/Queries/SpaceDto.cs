using System;
using MarchNote.Domain.Shared;
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
        public Guid? BackgroundImageId { get; set; }
        public string BackgroundColor { get; set; }
        public SpaceType Type { get; set; }
        public Visibility Visibility { get; set; }
    }
}