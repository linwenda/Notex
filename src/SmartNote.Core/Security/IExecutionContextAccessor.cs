using System;

namespace SmartNote.Core.Security
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }
        bool IsAvailable { get; }
    }
}