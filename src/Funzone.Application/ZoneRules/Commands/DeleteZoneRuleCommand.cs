using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.ZoneRules.Commands
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