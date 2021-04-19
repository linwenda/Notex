using System;

namespace Funzone.Application.Commands.Zones.CreateZone
{
    public class CreateZoneCommand : ICommand<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}