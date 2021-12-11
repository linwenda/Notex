using System;

namespace SmartNote.Core.Domain
{
    public interface IHasCreator
    {
        Guid CreatorId { get; set; }
    }
}