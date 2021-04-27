using System;

namespace Funzone.Application.Queries.ZoneMembers
{
    public class GetZoneMemberCountQuery : IQuery<int>
    {
        public GetZoneMemberCountQuery(Guid zoneId)
        {
            ZoneId = zoneId;
        }
        
        public Guid ZoneId { get; }
    }
}