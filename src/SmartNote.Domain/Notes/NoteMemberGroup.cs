namespace SmartNote.Domain.Notes
{
    public record NoteMemberGroup
    {
        private readonly List<NoteMember> _members;

        public NoteMemberGroup(List<NoteMember> members)
        {
            _members = members;
        }

        public void AddMember(Guid userId, NoteMemberRole role)
        {
            if (IsMember(userId)) return;

            _members.Add(new NoteMember(userId, role, DateTime.UtcNow, true, null));
        }

        public void RemoveMember(Guid userId)
        {
            if (!IsMember(userId)) return;

            var member = _members.First(u => u.MemberId == userId);

            _members.Remove(member);
        }

        public bool IsMember(Guid userId)
        {
            return _members.Any(m => m.IsActive && m.MemberId == userId);
        }

        public bool IsWriter(Guid userId)
        {
            return InRole(userId, NoteMemberRole.Writer);
        }

        public bool IsOwner(Guid userId)
        {
            return InRole(userId, NoteMemberRole.Author);
        }

        public bool InRole(Guid userId, NoteMemberRole role)
        {
            return _members.Any(m => m.IsActive && m.MemberId == userId && m.Role == role);
        }

        public List<NoteMemberSnapshot> GetMemberListSnapshot()
        {
            return _members.Select(m => m.ToSnapshot()).ToList();
        }
    }
}