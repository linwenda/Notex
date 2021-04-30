using System;
using Funzone.Application.Configuration.Queries;

namespace Funzone.Application.ZoneMembers.Queries
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