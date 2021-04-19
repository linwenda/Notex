using System;
using Funzone.Application.Contract;

namespace Funzone.Application.Queries.Zones
{
    public class GetZoneMembersCountQuery : IQuery<int>
    {
        public GetZoneMembersCountQuery(Guid zoneId)
        {
            ZoneId = zoneId;
        }

        public Guid ZoneId { get; }
    }
}