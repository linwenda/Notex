using System.Threading.Tasks;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;
using Microsoft.EntityFrameworkCore;

namespace Funzone.Infrastructure.DataAccess.Repositories
{
    public class ZoneMemberRepository : IZoneMemberRepository
    {
        private readonly FunzoneDbContext _context;

        public ZoneMemberRepository(FunzoneDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<ZoneMember> FindAsync(ZoneId zoneId, UserId userId)
        {
            return await _context.ZoneMembers.FirstOrDefaultAsync(z => z.ZoneId == zoneId && z.UserId == userId);
        }

        public async Task AddAsync(ZoneMember zoneMember)
        {
            await _context.ZoneMembers.AddAsync(zoneMember);
        }
    }
}