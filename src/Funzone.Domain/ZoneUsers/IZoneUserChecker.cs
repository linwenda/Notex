using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Domain.ZoneUsers
{
    public interface IZoneUserChecker
    {
        bool InZone(ZoneId zoneId, UserId userId);
    }
}