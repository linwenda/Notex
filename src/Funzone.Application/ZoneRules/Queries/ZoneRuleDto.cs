using System;

namespace Funzone.Application.ZoneRules.Queries
{
    public class ZoneRuleDto
    {
        public Guid Id { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public Guid AuthorId { get; private set; }
        public Guid ZoneId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Sort { get; private set; }
    }
}