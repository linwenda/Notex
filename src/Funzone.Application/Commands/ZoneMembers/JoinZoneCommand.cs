using System;

namespace Funzone.Application.Commands.ZoneUsers
{
    public class JoinZoneCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
    }
}