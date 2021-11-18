using System;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Notes.ReadModels
{
    public class NoteHistoryReadModel : IReadModelEntity,IHasCreationTime
    {
        public Guid Id { get; set; }
        public Guid NoteId { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreationTime { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Version { get; set; }
        public string Comment { get; set; }
    }
}