using System;
using Funzone.BuildingBlocks.Domain;

namespace Funzone.IdentityAccess.Domain.Users
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value) : base(value)
        {
        }
    }
}