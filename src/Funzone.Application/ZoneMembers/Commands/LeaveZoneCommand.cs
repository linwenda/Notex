using System;
using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.ZoneMembers.Commands
{
    public class LeaveZoneCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
    }
}