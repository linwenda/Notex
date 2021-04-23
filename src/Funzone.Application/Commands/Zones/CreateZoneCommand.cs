using System;

namespace Funzone.Application.Commands.Zones
{
    public class CreateZoneCommand : ICommand<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AvatarUrl { get; set; }
    }
}