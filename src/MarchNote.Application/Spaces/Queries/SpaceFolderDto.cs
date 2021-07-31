using System;

namespace MarchNote.Application.Spaces.Queries
{
    public class SpaceFolderDto
    {
        public Guid Id { get; set; }
        public Guid SpaceId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
    }
}