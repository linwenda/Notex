using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Users
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value) : base(value)
        {
        }
    }
}