using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using Funzone.Domain.ZoneUsers.Events;
using Funzone.Domain.ZoneUsers.Rules;

namespace Funzone.Domain.ZoneUsers
{
    public class ZoneUser : Entity, IAggregateRoot
    {
        public ZoneId ZoneId { get; private set; }
        public UserId UserId { get; private set; }
        public ZoneUserRole Role { get; private set; }
        public DateTime JoinedTime { get; private set; }

        public bool IsLeave { get; private set; }

        private ZoneUser()
        {
        }

        public ZoneUser(
            ZoneId zoneId,
            UserId userId,
            ZoneUserRole zoneRole)
        {
            ZoneId = zoneId;
            UserId = userId;
            Role = zoneRole;
            JoinedTime = DateTime.UtcNow;
            AddDomainEvent(new UserJoinedZoneDomainEvent(zoneId, userId));
        }

        public bool IsModerator()
        {
            return Role == ZoneUserRole.Moderator || Role == ZoneUserRole.Administrator;
        }

        public void Leave()
        {
            CheckRule(new ZoneAdministratorCannotLeaveRule(Role));
            IsLeave = true;
        }

        public void Rejoin()
        {
            CheckRule(new ZoneUserCannotRejoinRule(IsLeave));
            IsLeave = false;
            Role = ZoneUserRole.Member;
        }
    }
}