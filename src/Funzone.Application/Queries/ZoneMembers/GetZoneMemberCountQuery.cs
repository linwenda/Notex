using System;

namespace Funzone.Application.Queries.ZoneMembers
{
    public class GetZoneUserCountQuery : IQuery<int>
    {
        public GetZoneUserCountQuery(Guid zoneId)
        {
            ZoneId = zoneId;
        }
        
        public Guid ZoneId { get; }
    }
}