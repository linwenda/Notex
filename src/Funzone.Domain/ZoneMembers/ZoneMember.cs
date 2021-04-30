using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers.Events;
using Funzone.Domain.ZoneMembers.Rules;
using Funzone.Domain.Zones;

namespace Funzone.Domain.ZoneMembers
{
    public class ZoneMember : Entity, IAggregateRoot
    {
        public ZoneMemberId Id { get; private set; }
        public ZoneId ZoneId { get; private set; }
        public UserId UserId { get; private set; }
        public ZoneMemberRole Role { get; private set; }
        public DateTime JoinedTime { get; private set; }
        public bool IsLeave { get; private set; }

        private ZoneMember()
        {
        }

        public ZoneMember(
            ZoneId zoneId,
            UserId userId,
            ZoneMemberRole zoneRole)
        {
            ZoneId = zoneId;
            UserId = userId;
            Role = zoneRole;

            Id = new ZoneMemberId(Guid.NewGuid());
            JoinedTime = SystemClock.Now;
            
            AddDomainEvent(new UserJoinedZoneDomainEvent(zoneId, userId));
        }

        public bool IsModerator()
        {
            if (IsLeave) return false;

            return Role == ZoneMemberRole.Moderator || Role == ZoneMemberRole.Administrator;
        }

        public void Leave()
        {
            IsLeave = true;
        }

        public void Rejoin()
        {
            CheckRule(new ZoneMemberCannotRejoinRule(IsLeave));
            IsLeave = false;
            Role = ZoneMemberRole.Member;
        }

        public void PromoteToModerator(ZoneMember currentMember)
        {
            CheckRule(new ZoneMemberOnlyAdministratorCanPromotedToModeratorRule(currentMember));
            CheckRule(new ZoneMemberOnlyMemberCanBePromotedToModeratorRule(Role));
            
            Role = ZoneMemberRole.Moderator;
        }
    }
}