using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Users
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value) : base(value)
        {
        }
    }
}