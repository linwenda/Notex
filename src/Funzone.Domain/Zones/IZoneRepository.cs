using System.Threading.Tasks;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones
{
    public interface IZoneRepository : IRepository<Zone>
    {
        Task<Zone> GetByIdAsync(ZoneId id);
        Task AddAsync(Zone zone);
    }
}