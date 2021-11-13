﻿using System;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Notes.ReadModels
{
    public class NoteReadModel : IReadModelEntity, IHasCreationTime
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? ForkId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid SpaceId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Version { get; set; }
        public NoteStatus Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}