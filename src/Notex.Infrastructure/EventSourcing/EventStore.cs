using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Notex.Core.Domain.SeedWork;
using Notex.Messages;

namespace Notex.Infrastructure.EventSourcing;

public class EventStore<TEventSourcingDbContext> : IEventStore
    where TEventSourcingDbContext : DbContext, IEventSourcingDbContext
{
    private readonly TEventSourcingDbContext _dbContext;
    
    public EventStore(TEventSourcingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<IVersionedEvent>> LoadAsync(Guid sourcedId, int minVersion,
        CancellationToken cancellationToken = default)
    {
        var events = await _dbContext.Set<EventEntity>()
            .Where(x => x.SourcedId == sourcedId)
            .Where(x => x.Version >= minVersion)
            .OrderBy(x => x.Version)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        
        return events.Select(e => JsonConvert.DeserializeObject<IVersionedEvent>(
            e.Payload, EventSourcedSetting.JsonSerializerSettings)).ToList();
    }

    public async Task SaveAsync(IEnumerable<IVersionedEvent> events, CancellationToken cancellationToken = default)
    {
        var eventEntities = events.Select(e => new EventEntity
        {
            SourcedId = e.SourcedId,
            Version = e.Version,
            CreationTime = DateTime.UtcNow,
            Type = e.GetType().FullName,
            Payload = JsonConvert.SerializeObject(e, EventSourcedSetting.JsonSerializerSettings)
        });

        await _dbContext.Set<EventEntity>().AddRangeAsync(eventEntities, cancellationToken).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}