using System;

namespace MarchNote.Domain.Shared
{
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; }
    }
}