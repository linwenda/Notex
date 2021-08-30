using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.SeedWork.EventSourcing;
using MarchNote.Infrastructure.Events;
using MarchNote.Infrastructure.EventStore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MarchNote.Infrastructure.Repositories
{
    public class EventSourcedRepository<TEntity, TEntityId> : IEventSourcedRepository<TEntity, TEntityId>
        where TEntity : IEventSourcedEntity<TEntityId>
        where TEntityId : TypedIdValueBase
    {
        private readonly IMediator _mediator;
        private readonly MarchNoteDbContext _context;

        protected EventSourcedRepository(
            MarchNoteDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<TEntity> LoadAsync(TEntityId entityId, int version)
        {
            var maxVersion = version <= 0 ? int.MaxValue : version;

            var snapshot = await GetLatestSnapshot(entityId, maxVersion);

            var minVersion = snapshot?.EntityVersion + 1 ?? 0;

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

        public async Task SaveAsync(TEntity entity)
        {
            var uncommittedDomainEvents = entity.GetUnCommittedEvents();

            if (uncommittedDomainEvents.Any())
            {
                var eventEntities = uncommittedDomainEvents.Select(e => new EventEntity
                {
                    EntityId = entity.Id.Value,
                    EntityVersion = entity.Version,
                    Timestamp = DateTime.UtcNow,
                    Type = e.GetType().FullName,
                    Data = JsonConvert.SerializeObject(e)
                });

                await _context.Set<EventEntity>().AddRangeAsync(eventEntities);
            }

            var uncommittedSnapshot = entity.GetUnCommittedSnapshot();
            if (uncommittedSnapshot != null)
            {
                await _context.Set<SnapshotEntity>().AddAsync(new SnapshotEntity
                {
                    EntityId = entity.Id.Value,
                    EntityVersion = entity.Version,
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

        private static TEntity CreateEventSourcedEntity(TEntityId entityId)
        {
            var args = new object[] {entityId};
            var entity = (TEntity) Activator.CreateInstance(typeof(TEntity),
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, args,
                CultureInfo.CurrentCulture);

            return entity;
        }

        private async Task<List<IDomainEvent>> GetDomainEvents(TEntityId entityId, int minVersion, int maxVersion)
        {
            var assembly = typeof(IDomainEvent).Assembly;

            var events = await _context.Set<EventEntity>()
                .Where(x => x.EntityId == entityId.Value)
                .Where(x => x.EntityVersion >= minVersion)
                .Where(x => x.EntityVersion <= maxVersion)
                .OrderBy(x => x.EntityVersion)
                .ToListAsync()
                .ConfigureAwait(false);

            return events.Select(e => JsonConvert.DeserializeObject(e.Data,
                assembly.GetType(e.Type)) as IDomainEvent).ToList();
        }

        private async Task<ISnapshot> GetLatestSnapshot(TEntityId entityId, int maxVersion)
        {
            var assembly = typeof(ISnapshot).Assembly;

            var snapshot = await _context.Set<SnapshotEntity>()
                .Where(x => x.EntityId == entityId.Value)
                .Where(x => x.EntityVersion <= maxVersion)
                .OrderByDescending(x => x.EntityVersion)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return snapshot == null
                ? null
                : JsonConvert.DeserializeObject(snapshot.Data, assembly.GetType(snapshot.Type)) as ISnapshot;
        }
    }
}