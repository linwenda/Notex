using System;

namespace Funzone.Application.Configuration
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }

        bool IsAvailable { get; }
    }
}