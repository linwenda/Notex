using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.Zones.Commands
{
    public class CloseZoneCommand : ICommand<bool>
    {
        public CloseZoneCommand(Guid zoneId)
        {
            ZoneId = zoneId;
        }

        public Guid ZoneId { get;  }
    }
}