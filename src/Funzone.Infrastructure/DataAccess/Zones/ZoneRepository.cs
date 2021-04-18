using System.Threading.Tasks;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Zones;
using Microsoft.EntityFrameworkCore;

namespace Funzone.Infrastructure.DataAccess.Zones
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly FunzoneDbContext _context;

        public ZoneRepository(FunzoneDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;  

        public async Task<Zone> GetByIdAsync(ZoneId id)
        {
            return await _context.Zones.FirstOrDefaultAsync(z => z.Id == id);
        }

        public async Task AddAsync(Zone zone)
        {
            await _context.Zones.AddAsync(zone);
        }
    }
}