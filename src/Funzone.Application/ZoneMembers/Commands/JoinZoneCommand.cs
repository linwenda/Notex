using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.ZoneMembers.Commands
{
    public class JoinZoneCommand : ICommand<bool>
    {
        public JoinZoneCommand(Guid zoneId)
        {
            ZoneId = zoneId;
        }

        public Guid ZoneId { get; }
    }
}