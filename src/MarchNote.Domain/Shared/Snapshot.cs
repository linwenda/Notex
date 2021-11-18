using System;

namespace MarchNote.Domain.Shared
{
    public abstract class Snapshot : ISnapshot
    {
        public Guid EntityId { get; }
        public int EntityVersion { get; }

        protected Snapshot(Guid entityId, int entityVersion)
        {
            EntityId = entityId;
            EntityVersion = entityVersion;
        }
    }
}