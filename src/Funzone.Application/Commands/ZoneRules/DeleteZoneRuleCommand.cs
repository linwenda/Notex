using System;

namespace Funzone.Application.Commands.ZoneRules
{
    public class DeleteZoneRuleCommand : ICommand<bool>
    {
        public Guid ZoneRuleId { get; set; }
    }
}