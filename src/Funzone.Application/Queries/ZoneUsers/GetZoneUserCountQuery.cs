using System;

namespace Funzone.Application.Queries.ZoneUsers
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