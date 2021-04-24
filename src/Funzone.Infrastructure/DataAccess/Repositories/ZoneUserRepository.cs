using System.Threading.Tasks;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using Funzone.Domain.ZoneUsers;
using Microsoft.EntityFrameworkCore;

namespace Funzone.Infrastructure.DataAccess.Repositories
{
    public class ZoneUserRepository : IZoneUserRepository
    {
        private readonly FunzoneDbContext _context;

        public ZoneUserRepository(FunzoneDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<ZoneUser> GetAsync(ZoneId zoneId, UserId userId)
        {
            return await _context.ZoneUsers.FirstOrDefaultAsync(z => z.ZoneId == zoneId && z.UserId == userId);
        }

        public async Task AddAsync(ZoneUser zoneMember)
        {
            await _context.ZoneUsers.AddAsync(zoneMember);
        }
    }
}