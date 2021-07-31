using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Spaces
{
    public class SpaceId : TypedIdValueBase
    {
        public SpaceId(Guid value) : base(value)
        {
        }
    }
}