using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneRules.Rules;
using Funzone.Domain.Zones;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Domain.ZoneRules
{
    public class ZoneRule : Entity, IAggregateRoot
    {
        public ZoneRuleId Id { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public UserId AuthorId { get; private set; }
        public ZoneId ZoneId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Sort { get; private set; }
        public bool IsDeleted { get; private set; }

        private ZoneRule()
        {
        }

        public ZoneRule(
            ZoneUser zoneUser,
            string title,
            string description,
            int sort)
        {
            CheckRule(new ZoneRuleCannotAddedByMemberRule(zoneUser.Role));

            ZoneId = zoneUser.ZoneId;
            AuthorId = zoneUser.UserId;
            Title = title;
            Description = description;
            Sort = sort;

            Id = new ZoneRuleId(Guid.NewGuid());
            CreatedTime = Clock.Now;
        }

        public void Edit(
            ZoneUser zoneUser,
            string title,
            string description,
            int sort)
        {
            CheckRule(new ZoneRuleCannotEditedByMemberRule(zoneUser.Role));
            Title = title;
            Description = description;
            Sort = sort;
        }

        public void Delete(ZoneUser zoneUser)
        {
            CheckRule(new ZoneRuleCannotDeletedByMemberRule(zoneUser.Role));
            IsDeleted = true;
        }
    }
}