using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Zones;

namespace Funzone.Application.Queries.Zones
{
    public class GetZoneMembersCountQueryHandler : IQueryHandler<GetZoneMembersCountQuery,int>
    {
        private readonly IZoneCounter _zoneCounter;

        public GetZoneMembersCountQueryHandler(IZoneCounter zoneCounter)
        {
            _zoneCounter = zoneCounter;
        }
        
        public Task<int> Handle(GetZoneMembersCountQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_zoneCounter.CountZoneMembersWithId(new ZoneId(request.ZoneId)));
        }
    }
}