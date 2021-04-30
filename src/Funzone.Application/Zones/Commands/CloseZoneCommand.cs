using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.Zones.Commands
{
    public class CloseZoneCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
    }
}