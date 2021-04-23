using System;

namespace Funzone.Application.Commands.Zones
{
    public class CloseZoneCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
    }
}