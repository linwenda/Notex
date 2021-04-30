using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.ZoneMembers.Commands
{
    public class JoinZoneCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
    }
}