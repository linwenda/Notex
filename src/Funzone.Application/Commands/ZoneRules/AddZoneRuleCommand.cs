using System;

namespace Funzone.Application.Commands.ZoneRules
{
    public class AddZoneRuleCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Sort { get; set; }
    }
}