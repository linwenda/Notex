using System;

namespace Funzone.Application.Queries.Zones
{
    public class GetZoneByIdQuery : IQuery<ZoneDto>
    {
        public GetZoneByIdQuery(Guid zoneId)
        {
            ZoneId = zoneId;
        }

        public Guid ZoneId { get; }
    }
}