using System;
using MarchNote.Domain.SeedWork.Aggregates;

namespace MarchNote.Domain.NoteAggregate.ReadModels
{
    public class NoteHistoryReadModel : IReadModelEntity
    {
        public Guid Id { get; set; }
        public Guid NoteId { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime AuditedAt { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Version { get; set; }
        public string Comment { get; set; }
    }
}