﻿using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Notes.ReadModels
{
    public class NoteReadModel : IReadModelEntity
    {
        public Guid Id { get; set; }
        public Guid? FromId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid SpaceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Version { get; set; }
        public NoteStatus Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}