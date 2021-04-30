using System;

namespace Funzone.Application.Commands.Zones
{
    public class EditZoneCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
        public string Description { get; set; }
        public string AvatarUrl { get; set; }
    }
}