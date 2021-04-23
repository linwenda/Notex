using System;

namespace Funzone.Application.Commands.Zones
{
    public class AddZoneRuleCommand : ICommand<bool>
    {
        public AddZoneRuleCommand(Guid zoneId, string title, string description)
        {
            ZoneId = zoneId;
            Title = title;
            Description = description;
        }

        public Guid ZoneId { get; }
        public string Title { get; }
        public string Description { get; }
    }
}