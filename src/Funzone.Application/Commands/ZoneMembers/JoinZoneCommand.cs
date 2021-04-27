using System;

namespace Funzone.Application.Commands.ZoneMembers
{
    public class JoinZoneCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
    }
}