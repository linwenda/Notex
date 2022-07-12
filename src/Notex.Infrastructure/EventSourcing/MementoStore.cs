using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Notex.Core.Domain.SeedWork;

namespace Notex.Infrastructure.EventSourcing;

public class MementoStore<TEventSourcingDbContext> : IMementoStore
    where TEventSourcingDbContext : DbContext, IEventSourcingDbContext
{
    private readonly TEventSourcingDbContext _dbContext;

    public MementoStore(TEventSourcingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IMemento> GetLatestMementoAsync(Guid sourcedId, CancellationToken cancellationToken = default)
    {
        var latestMementoRecord = await _dbContext.Set<MementoEntity>()
            .Where(x => x.SourcedId == sourcedId)
            .OrderByDescending(x => x.Version)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (latestMementoRecord != null)
        {
            return JsonConvert.DeserializeObject<IMemento>(latestMementoRecord.Payload,
                EventSourcedSetting.JsonSerializerSettings);
        }

        return null;
    }

    public async Task SaveAsync(IMemento memento, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<MementoEntity>().AddAsync(new MementoEntity
        {
            SourcedId = memento.SourcedId,
            Version = memento.Version,
            Payload = JsonConvert.SerializeObject(memento, EventSourcedSetting.JsonSerializerSettings),
            Type = memento.GetType().FullName
        }, cancellationToken).ConfigureAwait(false);

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}