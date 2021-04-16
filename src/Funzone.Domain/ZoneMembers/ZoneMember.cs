using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Domain.ZoneMembers
{
    public class ZoneMember : Entity, IAggregateRoot
    {
        public ZoneId ZoneId { get; private set; }
        public UserId UserId { get; private set; }
        public ZoneRole Role { get; private set; }
        public DateTime JoinedTime { get; private set; }

        private ZoneMember()
        {
        }

        private ZoneMember(ZoneId zoneId, UserId userId, ZoneRole role)
        {
            ZoneId = zoneId;
            UserId = userId;
            Role = role;
            JoinedTime = DateTime.UtcNow;
        }

        public static ZoneMember Create(ZoneId zoneId,UserId userId)
        {
            return new ZoneMember(zoneId, userId, ZoneRole.Member);
        }

        public void SetCaptain()
        {
            Role = ZoneRole.Captain;
        }
    }
}