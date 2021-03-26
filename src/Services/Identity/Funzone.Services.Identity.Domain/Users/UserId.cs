using System;
using Funzone.BuildingBlocks.Domain;

namespace Funzone.Services.Identity.Domain.Users
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value) : base(value)
        {
        }
    }
}