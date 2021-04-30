using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.ZoneRules.Commands
{
    public class EditZoneRuleCommand : ICommand<bool>
    {
        public EditZoneRuleCommand(
            Guid zoneRuleId, 
            string title, 
            string description,
            int sort)
        {
            ZoneRuleId = zoneRuleId;
            Title = title;
            Description = description;
            Sort = sort;
        }

        public Guid ZoneRuleId { get; }
        public string Title { get; }
        public string Description { get;}
        public int Sort { get; }
    }
}