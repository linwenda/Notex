using System;

namespace MarchNote.Domain.Shared
{
    public interface ISnapshot
    {
        Guid EntityId { get; }
        int EntityVersion { get; }
    }
}