using Funzone.Domain.Users;

namespace Funzone.Domain.Zones
{
    public interface IZoneCounter
    {
        int CountZoneWithTitle(string title);
        int CountZoneMembersWithId(ZoneId id);
        int CountZoneMemberWithUserId(UserId userId);
    }
}