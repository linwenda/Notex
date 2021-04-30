using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.Zones.Commands
{
    public class CreateZoneCommand : ICommand<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AvatarUrl { get; set; }
    }
}