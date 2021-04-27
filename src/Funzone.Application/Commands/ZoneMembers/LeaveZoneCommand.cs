using System;

namespace Funzone.Application.Commands.ZoneMembers
{
    public class LeaveZoneCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
    }
}