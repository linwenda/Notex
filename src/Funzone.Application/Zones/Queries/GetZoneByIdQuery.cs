using System;
using Funzone.Application.Configuration.Queries;

namespace Funzone.Application.Zones.Queries
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