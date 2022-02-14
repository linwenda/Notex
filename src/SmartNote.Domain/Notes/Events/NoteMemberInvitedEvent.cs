using SmartNote.Core.Ddd;

namespace SmartNote.Domain.Notes.Events
{
    public class NoteMemberInvitedEvent : DomainEventBase
    {
        public Guid NoteId { get; }
        public Guid MemberId { get; }
        public string Role { get; }
        public DateTime JoinTime { get; }

        public NoteMemberInvitedEvent(
            Guid noteId,
            Guid memberId,
            string role,
            DateTime joinTime)
        {
            NoteId = noteId;
            MemberId = memberId;
            Role = role;
            JoinTime = joinTime;
        }
    }
}