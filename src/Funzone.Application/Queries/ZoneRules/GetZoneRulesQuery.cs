using System;
using System.Collections.Generic;

namespace Funzone.Application.Queries.ZoneRules
{
    public class GetZoneRulesQuery : IQuery<IEnumerable<ZoneRuleDto>>
    {
        public GetZoneRulesQuery(Guid zoneId)
        {
            ZoneId = zoneId;
        }

        public Guid ZoneId { get; }
    }
}