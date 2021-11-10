using System;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.Notes
{
    public record NoteMember
    {
        public Guid MemberId { get; }
        public NoteMemberRole Role { get; }
        public DateTime JoinedAt { get; }
        public bool IsActive { get; }
        public DateTime? LeaveAt { get; }

        public NoteMember(
            Guid memberId,
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
                MemberId,
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
                MemberId,
                NoteMemberRole.Of(Role),
                JoinedAt,
                IsActive,
                LeaveAt);
        }
    }
}