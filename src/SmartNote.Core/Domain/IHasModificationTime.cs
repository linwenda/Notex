using System;

namespace SmartNote.Core.Domain
{
    public interface IHasModificationTime
    {
        DateTimeOffset? LastModificationTime { get; set; }
    }
}