using System;

namespace Funzone.Application.Queries.ZoneUsers
{
    public class UserJoinZoneDto
    {
        public Guid ZoneId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime JoinedTime { get; set; }
        public string Role { get; set; }
    }
}