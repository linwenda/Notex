using System.Threading.Tasks;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Domain.ZoneUsers
{
    public interface IZoneUserRepository : IRepository<ZoneUser>
    {
        Task<ZoneUser> GetAsync(ZoneId zoneId, UserId userId);

        Task AddAsync(ZoneUser zoneMember);
    }
}