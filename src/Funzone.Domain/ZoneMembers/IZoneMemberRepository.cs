using System.Threading.Tasks;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Domain.ZoneMembers
{
    public interface IZoneMemberRepository : IRepository<ZoneMember>
    {
        Task<ZoneMember> FindAsync(ZoneId zoneId, UserId userId);
        Task AddAsync(ZoneMember zoneMember);
    }
}