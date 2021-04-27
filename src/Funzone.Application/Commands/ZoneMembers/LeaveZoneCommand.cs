using System;

namespace Funzone.Application.Commands.ZoneUsers
{
    public class LeaveZoneCommand : ICommand<bool>
    {
        public Guid ZoneId { get; set; }
    }
}