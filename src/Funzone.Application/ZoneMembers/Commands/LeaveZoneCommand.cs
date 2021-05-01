using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.ZoneMembers.Commands
{
    public class LeaveZoneCommand : ICommand<bool>
    {
        public LeaveZoneCommand(Guid zoneId)
        {
            ZoneId = zoneId;
        }

        public Guid ZoneId { get;  }
    }
}