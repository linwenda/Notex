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
        public ZoneRole Role { get; private set; }
        public DateTime JoinedTime { get; private set; }

        public bool IsLeave { get; private set; }

        private ZoneUser()
        {
        }

        private ZoneUser(
            ZoneId zoneId,
            UserId userId)
        {
            ZoneId = zoneId;
            UserId = userId;
            Role = ZoneRole.Member;
            JoinedTime = DateTime.UtcNow;
            AddDomainEvent(new UserJoinedZoneDomainEvent(zoneId, userId));
        }

        public static ZoneUser Create(ZoneId zoneId, UserId userId)
        {
            return new ZoneUser(zoneId, userId);
        }

        public bool IsModerator()
        {
            return Role == ZoneRole.Moderator || Role == ZoneRole.Administrator;
        }

        public bool IsAdministrator()
        {
            return Role == ZoneRole.Administrator;
        }

        public void Leave()
        {
            IsLeave = true;
        }

        public void Rejoin()
        {
            CheckRule(new ZoneUserCannotRejoinRule(IsLeave));
            IsLeave = false;
        }
    }
}