using System;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.NoteAggregate
{
    public record NoteMember
    {
        public UserId MemberId { get; set; }
        public NoteMemberRole Role { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LeaveAt { get; set; }

        public NoteMember(
            UserId memberId,
            NoteMemberRole role,
            DateTime joinedAt,
            bool isActive,
            DateTime? leaveAt)
        {
            MemberId = memberId;
            Role = role;
            JoinedAt = joinedAt;
            IsActive = isActive;
            LeaveAt = leaveAt;
        }

        public NoteMemberSnapshot ToSnapshot()
        {
            return new NoteMemberSnapshot(
                MemberId.Value,
                Role.Value,
                JoinedAt,
                IsActive,
                LeaveAt);
        }
    }

    public record NoteMemberSnapshot
    {
        public Guid MemberId { get; }
        public string Role { get; }
        public DateTime JoinedAt { get; }
        public bool IsActive { get; }
        public DateTime? LeaveAt { get; }

        public NoteMemberSnapshot(
            Guid memberId,
            string role,
            DateTime joinedAt,
            bool isActive,
            DateTime? leaveAt)
        {
            MemberId = memberId;
            Role = role;
            JoinedAt = joinedAt;
            IsActive = isActive;
            LeaveAt = leaveAt;
        }

        public NoteMember ToMember()
        {
            return new NoteMember(
                new UserId(MemberId),
                NoteMemberRole.Of(Role),
                JoinedAt,
                IsActive,
                LeaveAt);
        }
    }
}