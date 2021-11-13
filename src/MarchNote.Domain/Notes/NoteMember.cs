using System;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.Notes
{
    public record NoteMember
    {
        public Guid MemberId { get; }
        public NoteMemberRole Role { get; }
        public DateTime JoinTime { get; }
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
            JoinTime = joinedAt;
            IsActive = isActive;
            LeaveAt = leaveAt;
        }

        public NoteMemberSnapshot ToSnapshot()
        {
            return new NoteMemberSnapshot(
                MemberId,
                Role.Value,
                JoinTime,
                IsActive,
                LeaveAt);
        }
    }

    public record NoteMemberSnapshot
    {
        public Guid MemberId { get; }
        public string Role { get; }
        public DateTime JoinTime { get; }
        public bool IsActive { get; }
        public DateTime? LeaveAt { get; }

        public NoteMemberSnapshot(
            Guid memberId,
            string role,
            DateTime joinTime,
            bool isActive,
            DateTime? leaveAt)
        {
            MemberId = memberId;
            Role = role;
            JoinTime = joinTime;
            IsActive = isActive;
            LeaveAt = leaveAt;
        }

        public NoteMember ToMember()
        {
            return new NoteMember(
                MemberId,
                NoteMemberRole.Of(Role),
                JoinTime,
                IsActive,
                LeaveAt);
        }
    }
}