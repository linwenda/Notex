using System;
using System.Collections.Generic;
using System.Linq;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.Notes
{
    public record NoteMemberGroup
    {
        private readonly List<NoteMember> _members;

        public NoteMemberGroup(List<NoteMember> members)
        {
            _members = members;
        }

        public void AddMember(UserId userId, NoteMemberRole role)
        {
            if (IsMember(userId)) return;

            _members.Add(new NoteMember(userId, role, DateTime.UtcNow, true, null));
        }

        public void RemoveMember(UserId userId)
        {
            if (!IsMember(userId)) return;

            var member = _members.First(u => u.MemberId == userId);

            _members.Remove(member);
        }

        public bool IsMember(UserId userId)
        {
            return _members.Any(m => m.IsActive && m.MemberId == userId);
        }

        public bool IsWriter(UserId userId)
        {
            return InRole(userId, NoteMemberRole.Writer);
        }

        public bool IsOwner(UserId userId)
        {
            return InRole(userId, NoteMemberRole.Owner);
        }

        public bool InRole(UserId userId, NoteMemberRole role)
        {
            return _members.Any(m => m.IsActive && m.MemberId == userId && m.Role == role);
        }

        public List<NoteMemberSnapshot> GetMemberListSnapshot()
        {
            return _members.Select(m => m.ToSnapshot()).ToList();
        }
    }
}