using System.Threading.Tasks;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Domain.SeedWork;
using Funzone.Domain.ZoneRules;
using Microsoft.EntityFrameworkCore;

namespace Funzone.Infrastructure.DataAccess.Repositories
{
    public class ZoneRuleRepository : IZoneRuleRepository
    {
        private readonly FunzoneDbContext _context;

        public ZoneRuleRepository(FunzoneDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<ZoneRule> GetByIdAsync(ZoneRuleId id)
        {
            var zoneRule =await _context.ZoneRules.FirstOrDefaultAsync(r => r.Id == id);
            return zoneRule ?? throw new NotFoundException(nameof(ZoneRule), id);
        }

        public async Task AddAsync(ZoneRule zoneRule)
        {
            await _context.ZoneRules.AddAsync(zoneRule);
        }
    }
}