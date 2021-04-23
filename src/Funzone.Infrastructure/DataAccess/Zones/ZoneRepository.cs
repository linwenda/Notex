using System.Linq;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Exceptions;
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
            var zone = await _context.Zones.FirstOrDefaultAsync(z => z.Id == id);
            return zone ?? throw new NotFoundException(nameof(Zone), id);
        }

        public async Task AddAsync(Zone zone)
        {
            await _context.Zones.AddAsync(zone);
        }

        public void Update(Zone zone)
        {
            _context.Entry(zone).State = EntityState.Modified;
        }
    }
}