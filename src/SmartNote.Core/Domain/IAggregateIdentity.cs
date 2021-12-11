using System;

namespace SmartNote.Core.Domain
{
    public interface IAggregateIdentity
    {
        Guid Value { get; }
    }
}