using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.Zones.Commands
{
    public class EditZoneCommand : ICommand<bool>
    {
        public EditZoneCommand(Guid zoneId, string description, string avatarUrl)
        {
            ZoneId = zoneId;
            Description = description;
            AvatarUrl = avatarUrl;
        }

        public Guid ZoneId { get; }
        public string Description { get; }
        public string AvatarUrl { get; }
    }
}