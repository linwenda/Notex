using System;

namespace SmartNote.Core.Domain
{
    public interface ISnapshot
    {
        Guid AggregateId { get; }
        int AggregateVersion { get; }
    }
}