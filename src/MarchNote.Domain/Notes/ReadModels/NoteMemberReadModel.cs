using System;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Notes.ReadModels
{
    public class NoteMemberReadModel : IReadModelEntity
    {
        public Guid NoteId { get; set; }
        public Guid MemberId { get; set; }
        public string Role { get; set; }
        public DateTime JoinedAt { get; set; }
        public DateTime? LeaveAt { get; set; }
        public bool IsActive { get; set; }
    }
}