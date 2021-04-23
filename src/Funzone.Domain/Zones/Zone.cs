using System;
using System.Collections.Generic;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.Zones.Events;
using Funzone.Domain.Zones.Rules;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Domain.Zones
{
    public class Zone : Entity, IAggregateRoot, IHaveAuthorId
    {
        public ZoneId Id { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public UserId AuthorId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public ZoneStatus Status { get; private set; }
        public string AvatarUrl { get; private set; }
        public List<ZoneRule> Rules { get; private set; }

        private Zone()
        {
        }

        private Zone(
            IZoneCounter zoneCounter,
            UserId authorId,
            string title,
            string description,
            string avatarUrl) : this()
        {
            CheckRule(new ZoneTitleMustBeUniqueRule(zoneCounter, title));

            Id = new ZoneId(Guid.NewGuid());
            CreatedTime = DateTime.UtcNow;
            AuthorId = authorId;
            Title = title;
            Description = description;
            AvatarUrl = avatarUrl;
            Status = ZoneStatus.Active;

            AddDomainEvent(new ZoneCreatedDomainEvent(this));
        }

        public static Zone Create(
            IZoneCounter zoneCounter,
            UserId authorId,
            string title,
            string description,
            string avatarUrl)
        {
            return new Zone(
                zoneCounter,
                authorId,
                title,
                description,
                avatarUrl);
        }

        public void Close(UserId userId)
        {
            CheckRule(new ZoneCanBeClosedOnlyByAuthorRule(this, userId));
            Status = ZoneStatus.Closed;
        }

        public void Edit(UserId userId, string description, string avatarUrl)
        {
            CheckRule(new ZoneCanBeEditedOnlyByAuthorRule(this, userId));
            Description = description;
            AvatarUrl = avatarUrl;
        }

        public ZoneUser Join(UserId userId)
        {
            return new ZoneUser(Id, userId, ZoneRole.Member);  
        } 

        public ZoneUser AddAdministrator()
        {
            return new ZoneUser(Id, AuthorId, ZoneRole.Administrator);
        }

        public void AddRule(
            ZoneUser member,
            string title,
            string description)
        {
            Rules.Add(ZoneRule.Create(member, title, description));
        }
    }
}