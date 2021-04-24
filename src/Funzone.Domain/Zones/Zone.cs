using System;
using System.Collections.Generic;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneRules;
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
            CheckRule(new ZoneMustBeActivatedRule(Status));
            CheckRule(new ZoneCanBeClosedOnlyByAuthorRule(this, userId));
            Status = ZoneStatus.Closed;
        }

        public void Edit(UserId userId, string description, string avatarUrl)
        {
            CheckRule(new ZoneMustBeActivatedRule(Status));
            CheckRule(new ZoneCanBeEditedOnlyByAuthorRule(this, userId));
            Description = description;
            AvatarUrl = avatarUrl;
        }

        public ZoneUser Join(UserId userId)
        {
            CheckRule(new ZoneMustBeActivatedRule(Status));
            return new ZoneUser(Id, userId, ZoneUserRole.Member);
        }

        public ZoneUser AddAdministrator()
        {
            CheckRule(new ZoneMustBeActivatedRule(Status));
            return new ZoneUser(Id, AuthorId, ZoneUserRole.Administrator);
        }

        public ZoneRule AddRule(ZoneUser zoneUser, string title, string description, int sort)
        {
            CheckRule(new ZoneMustBeActivatedRule(Status));
            return new ZoneRule(zoneUser, title, description, sort);
        }
    }
}