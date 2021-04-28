using System.Threading.Tasks;
using Funzone.Domain.Zones;

namespace Funzone.Domain.ZoneMembers
{
    public interface IZoneMemberContext
    {
        Task<ZoneMember> Member(ZoneId zoneId);
    }
}
