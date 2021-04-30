using System.Threading.Tasks;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Zones;

namespace Funzone.Domain.ZoneMembers
{
    public interface IZoneMemberRepository : IRepository<ZoneMember>
    {
        Task<ZoneMember> GetByIdAsync(ZoneMemberId id);
        Task AddAsync(ZoneMember zoneMember);
        Task<ZoneMember> GetCurrentMember(ZoneId zoneId);
    }
}