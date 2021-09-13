using System;

namespace MarchNote.Domain.SeedWork
{
    public interface ISnapshot
    {
        Guid EntityId { get; }
        int EntityVersion { get; }
    }
}