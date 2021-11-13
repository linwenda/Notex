using System;

namespace MarchNote.Domain.Shared
{
    public interface IHasModificationTime
    {
        DateTime? LastModificationTime { get; }
    }
}