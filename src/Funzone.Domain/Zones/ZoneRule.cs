using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Zones.Rules;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Domain.Zones
{
    public class ZoneRule : Entity
    {
        public ZoneId ZoneId { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        private ZoneRule()
        {
        }

        private ZoneRule(
            ZoneUser zoneMember,
            string title,
            string description)
        {
            CheckRule(new ZoneRuleCanBeAddedOnlyByModeratorRule(zoneMember));

            ZoneId = zoneMember.ZoneId;
            Title = title;
            Description = description;
            CreatedTime = DateTime.UtcNow;
        }

        public static ZoneRule Create(
            ZoneUser zoneMember,
            string title,
            string description)
        {
            return new ZoneRule(zoneMember, title, description);
        }

        public void Edit(ZoneUser zoneMember, string title, string description)
        {
            CheckRule(new ZoneRuleCanBeAddedOnlyByModeratorRule(zoneMember));

            Title = title;
            Description = description;
        }
    }
}