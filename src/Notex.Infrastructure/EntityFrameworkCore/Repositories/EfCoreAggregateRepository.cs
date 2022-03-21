using System.Globalization;
using System.Reflection;
using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Aggregates;
using Notex.Core.Lifetimes;
using Notex.Messages;

namespace Notex.Infrastructure.EntityFrameworkCore.Repositories;

public class EfCoreAggregateRepository : IAggregateRepository, IScopedLifetime
{
    private readonly NotexDbContext _dbContext;
    private readonly IMediator _mediator;

    public EfCoreAggregateRepository(NotexDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<TAggregateRoot> LoadAsync<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot
    {
        var aggregateRoot = CreateAggregateRoot<TAggregateRoot>(id);

        var minVersion = 0;

        if (aggregateRoot is IMementoOriginator mementoOriginator)
        {
            var memento = await GetLatestMemento(id);

            if (memento != null)
            {
                minVersion = memento.AggregateVersion + 1;
                mementoOriginator.SetMemento(memento);
            }
        }

        var history = await GetEventHistory(id, minVersion);

        aggregateRoot.LoadFrom(history);

        return aggregateRoot;
    }

    public async Task SaveAsync<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
    {
        var uncommittedEvents = aggregateRoot.PopUncommittedEvents();

        if (!uncommittedEvents.Any()) return;

        var eventRecords = new List<EventRecord>();

        foreach (var uncommittedEvent in uncommittedEvents)
        {
            //Take snapshot every 10 version
            if (aggregateRoot.Version > 1 && uncommittedEvent.AggregateVersion % 10 == 0)
            {
                if (aggregateRoot is IMementoOriginator mementoOriginator)
                {
                    var memento = mementoOriginator.GetMemento();

                    await _dbContext.Set<MementoRecord>().AddAsync(new MementoRecord
                    {
                        AggregateId = memento.AggregateId,
                        AggregateVersion = memento.AggregateVersion,
                        Payload = JsonSerializer.SerializeToDocument(memento, memento.GetType()),
                        Type = memento.GetType().FullName
                    });
                }
            }

            eventRecords.Add(new EventRecord
            {
                AggregateId = uncommittedEvent.AggregateId,
                AggregateVersion = uncommittedEvent.AggregateVersion,
                CreationTime = DateTimeOffset.UtcNow,
                Type = uncommittedEvent.GetType().FullName,
                Payload = JsonSerializer.SerializeToDocument(uncommittedEvent, uncommittedEvent.GetType())
            });
        }

        await _dbContext.Set<EventRecord>().AddRangeAsync(eventRecords);
        await _dbContext.SaveChangesAsync();

        var tasks = uncommittedEvents.Select(e => _mediator.Publish(e)).ToList();
        await Task.WhenAll(tasks);
    }

    private static TAggregateRoot CreateAggregateRoot<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot
    {
        var args = new object[] {id};

        return (TAggregateRoot) Activator.CreateInstance(typeof(TAggregateRoot),
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, args,
            CultureInfo.CurrentCulture);
    }

    private async Task<IMemento> GetLatestMemento(Guid id)
    {
        var assembly = typeof(IMemento).Assembly;

        var latestMementoRecord = await _dbContext.Set<MementoRecord>()
            .Where(x => x.AggregateId == id)
            .OrderByDescending(x => x.AggregateVersion)
            .FirstOrDefaultAsync();

        return latestMementoRecord?.Payload.Deserialize(assembly.GetType(latestMementoRecord.Type)) as IMemento;
    }

    private async Task<List<IVersionedEvent>> GetEventHistory(Guid id, int version)
    {
        var assembly = typeof(IVersionedEvent).Assembly;

        var events = await _dbContext.Set<EventRecord>()
            .Where(x => x.AggregateId == id)
            .Where(x => x.AggregateVersion >= version)
            .OrderBy(x => x.AggregateVersion)
            .ToListAsync()
            .ConfigureAwait(false);


        return events.Select(e => e.Payload.Deserialize(assembly.GetType(e.Type)) as IVersionedEvent).ToList();
    }
}