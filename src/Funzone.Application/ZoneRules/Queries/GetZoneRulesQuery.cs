using System;
using System.Collections.Generic;
using Funzone.Application.Configuration.Queries;

namespace Funzone.Application.ZoneRules.Queries
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