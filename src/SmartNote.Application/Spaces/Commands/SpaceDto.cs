﻿using SmartNote.Domain.Spaces;

namespace SmartNote.Application.Spaces.Commands
{
    public class SpaceDto
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }

        public DateTimeOffset CreationTime { get; set; }

        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public Guid? BackgroundImageId { get; set; }
        public string BackgroundColor { get; set; }
        public SpaceType Type { get; set; }
        public Visibility Visibility { get; set; }
    }
}