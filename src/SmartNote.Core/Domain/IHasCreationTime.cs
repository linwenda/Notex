using System;

namespace SmartNote.Core.Domain
{
    public interface IHasCreationTime
    {
        DateTimeOffset CreationTime { get; set; }
    }
}