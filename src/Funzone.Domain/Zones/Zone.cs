using System;
using Funzone.Domain.PostDrafts;
using Funzone.Domain.Posts;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.ZoneRules;
using Funzone.Domain.Zones.Events;
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
            CreatedTime = Clock.Now;
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
            CheckRule(new ZoneCanBeClosedOnlyByAuthorRule(AuthorId, userId));
            Status = ZoneStatus.Closed;
        }

        public void Edit(UserId editorId, string description, string avatarUrl)
        {
            CheckRule(new ZoneMustBeActivatedRule(Status));
            CheckRule(new ZoneCanBeEditedOnlyByAuthorRule(AuthorId, editorId));
            Description = description;
            AvatarUrl = avatarUrl;
        }

        public ZoneMember Join(UserId userId)
        {
            CheckRule(new ZoneMustBeActivatedRule(Status));
            return new ZoneMember(Id, userId, ZoneMemberRole.Member);
        }

        public ZoneMember AddAdministrator()
        {
            CheckRule(new ZoneMustBeActivatedRule(Status));
            return new ZoneMember(Id, AuthorId, ZoneMemberRole.Administrator);
        }

        public ZoneRule AddRule(ZoneMember zoneMember, string title, string description, int sort)
        {
            CheckRule(new ZoneMustBeActivatedRule(Status));
            return new ZoneRule(zoneMember, title, description, sort);
        }

        public Post AddPost(UserId authorId, string title, string content,PostType type)
        {
            CheckRule(new ZoneMustBeActivatedRule(Status));
            return new Post(
                Id,
                authorId,
                title, 
                content,
                type);
        }

        public PostDraft AddPostDraft(ZoneMember zoneMember, string title, string content,PostType type)
        {
            CheckRule(new ZoneMustBeActivatedRule(Status));
            return new PostDraft(
                zoneMember,
                title,
                content,
                type);
        }
    }
}