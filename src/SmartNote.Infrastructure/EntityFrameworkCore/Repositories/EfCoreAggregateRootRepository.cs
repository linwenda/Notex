using System.Globalization;
using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartNote.Domain;
using SmartNote.Infrastructure.EntityFrameworkCore.EventStore;

namespace SmartNote.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfCoreAggregateRootRepository<TAggregateRoot, TAggregateIdentity> : IAggregateRootRepository<
        TAggregateRoot,
        TAggregateIdentity>
        where TAggregateRoot : IAggregateRoot<TAggregateIdentity>
        where TAggregateIdentity : IAggregateIdentity
    {
        private readonly IMediator _mediator;
        private readonly SmartNoteDbContext _context;

        public EfCoreAggregateRootRepository(IMediator mediator, SmartNoteDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<TAggregateRoot> LoadAsync(TAggregateIdentity entityId, int version)
        {
            var maxVersion = version <= 0 ? int.MaxValue : version;

            var snapshot = await GetLatestSnapshot(entityId, maxVersion);

            var minVersion = snapshot?.AggregateVersion + 1 ?? 0;

            var events = await GetDomainEvents(entityId, minVersion, maxVersion);

            var entity = CreateEventSourcedEntity(entityId);

            if (snapshot == null)
            {
                if (events.Count == 0)
                {
                    return default;
                }

                entity.Load(events);
            }
            else
            {
                entity.Load(snapshot, events);
            }

            return entity;
        }

        public async Task SaveAsync(TAggregateRoot aggregateRoot)
        {
            var uncommittedDomainEvents = aggregateRoot.GetUnCommittedEvents();

            if (uncommittedDomainEvents.Any())
            {
                var eventEntities = uncommittedDomainEvents.Select(e => new EventEntity
                {
                    AggregateId = aggregateRoot.Id.Value,
                    AggregateVersion = aggregateRoot.Version,
                    Timestamp = DateTime.UtcNow,
                    Type = e.GetType().FullName,
                    Data = JsonConvert.SerializeObject(e)
                });

                await _context.Set<EventEntity>().AddRangeAsync(eventEntities);
            }

            var uncommittedSnapshot = aggregateRoot.GetUnCommittedSnapshot();
            if (uncommittedSnapshot != null)
            {
                await _context.Set<SnapshotEntity>().AddAsync(new SnapshotEntity
                {
                    AggregateId = aggregateRoot.Id.Value,
                    AggregateVersion = aggregateRoot.Version,
                    Type = uncommittedSnapshot.GetType().FullName,
                    Data = JsonConvert.SerializeObject(uncommittedSnapshot),
                });
            }

            await _context.SaveChangesAsync();

            foreach (var uncommittedDomainEvent in uncommittedDomainEvents)
            {
                await _mediator.Publish(uncommittedDomainEvent);
            }
        }

        private static TAggregateRoot CreateEventSourcedEntity(TAggregateIdentity entityId)
        {
            var args = new object[] {entityId};
            var entity = (TAggregateRoot) Activator.CreateInstance(typeof(TAggregateRoot),
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, args,
                CultureInfo.CurrentCulture);

            return entity;
        }

        private async Task<List<IDomainEvent>> GetDomainEvents(TAggregateIdentity entityId, int minVersion,
            int maxVersion)
        {
            var assembly = typeof(IDomainEvent).Assembly;

            var events = await _context.Set<EventEntity>()
                .Where(x => x.AggregateId == entityId.Value)
                .Where(x => x.AggregateVersion >= minVersion)
                .Where(x => x.AggregateVersion <= maxVersion)
                .OrderBy(x => x.AggregateVersion)
                .ToListAsync()
                .ConfigureAwait(false);

            return events.Select(e => JsonConvert.DeserializeObject(e.Data,
                assembly.GetType(e.Type)) as IDomainEvent).ToList();
        }

        private async Task<ISnapshot> GetLatestSnapshot(TAggregateIdentity entityId, int maxVersion)
        {
            var assembly = typeof(ISnapshot).Assembly;

            var snapshot = await _context.Set<SnapshotEntity>()
                .Where(x => x.AggregateId == entityId.Value)
                .Where(x => x.AggregateVersion <= maxVersion)
                .OrderByDescending(x => x.AggregateVersion)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return snapshot == null
                ? null
                : JsonConvert.DeserializeObject(snapshot.Data, assembly.GetType(snapshot.Type)) as ISnapshot;
        }
    }
}