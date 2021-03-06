using Funzone.BuildingBlocks.Domain;
using System;

namespace Funzone.UserAccess.Domain.Users
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value) : base(value)
        {
        }
    }
}