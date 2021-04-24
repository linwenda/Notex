using System;

namespace Funzone.Application.Commands.ZoneRules
{
    public class AddZoneRuleCommand : ICommand<bool>
    {
        public AddZoneRuleCommand(Guid zoneId, string title, string description, int sort)
        {
            ZoneId = zoneId;
            Title = title;
            Description = description;
            Sort = sort;
        }

        public Guid ZoneId { get; }
        public string Title { get; }
        public string Description { get; }
        public int Sort { get; }
    }
}