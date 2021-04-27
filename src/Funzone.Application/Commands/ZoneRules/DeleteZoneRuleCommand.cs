using System;

namespace Funzone.Application.Commands.ZoneRules
{
    public class DeleteZoneRuleCommand : ICommand<bool>
    {
        public DeleteZoneRuleCommand(Guid zoneRuleId)
        {
            ZoneRuleId = zoneRuleId;
        }

        public Guid ZoneRuleId { get;}
    }
}