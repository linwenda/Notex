using Funzone.Domain.Users;

namespace Funzone.Domain.Zones
{
    public interface IZoneCounter
    {
        int CountZoneWithTitle(string title);
    }
}