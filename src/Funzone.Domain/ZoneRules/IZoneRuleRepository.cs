using System.Threading.Tasks;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.ZoneRules
{
    public interface IZoneRuleRepository : IRepository<ZoneRule>
    {
        Task<ZoneRule> GetByIdAsync(ZoneRuleId id);
        Task AddAsync(ZoneRule zoneRule);
    }
}