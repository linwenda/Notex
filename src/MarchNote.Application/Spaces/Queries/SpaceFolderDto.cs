using System;

namespace MarchNote.Application.Spaces.Queries
{
    public class SpaceFolderDto
    {
        public Guid Id { get; private set; }
        public Guid SpaceId { get; private set; }
        public Guid AuthorId { get; private set; }
        public Guid? ParentId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Name { get; private set; }
    }
}