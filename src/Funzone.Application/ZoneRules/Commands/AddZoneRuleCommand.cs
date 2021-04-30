using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.ZoneRules.Commands
{
    public class AddZoneRuleCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Sort { get; set; }
    }
}