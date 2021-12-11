using System;

namespace SmartNote.Core.Domain
{
    public interface IHasModifier
    {
        Guid? LastModifierId { get; set; }
    }
}