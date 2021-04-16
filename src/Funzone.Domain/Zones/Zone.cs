using System;
using System.Collections.Generic;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones.Rules;

namespace Funzone.Domain.Zones
{
    public class Zone : Entity, IAggregateRoot
    {
        public ZoneId Id { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public UserId AuthorId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public List<ZoneMember> ZoneMembers { get; private set; }

        private Zone()
        {
            ZoneMembers = new List<ZoneMember>();
        }

        private Zone(
            IZoneCounter zoneCounter,
            UserId authorId,
            string title, 
            string description) : this()
        {
            CheckRule(new ZoneTitleMustBeUniqueRule(zoneCounter, title));

            Id = new ZoneId(Guid.NewGuid());
            CreatedTime = DateTime.UtcNow;
            AuthorId = authorId;
            Title = title;
            Description = description;
        }

        public static Zone Create(
            IZoneCounter zoneCounter,
            UserId authorId, 
            string title,
            string description)
        {
            return new Zone(
                zoneCounter,
                authorId,
                title, 
                description);
        }

        public void Join(IZoneCounter zoneCounter, UserId userId)
        {
            CheckRule(new UserCannotRejoinRule(zoneCounter, userId));
            ZoneMembers.Add(ZoneMember.Create(Id, userId));
        }
    }
}