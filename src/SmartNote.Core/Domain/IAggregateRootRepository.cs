﻿using System.Threading.Tasks;

namespace SmartNote.Core.Domain
{
    public interface IAggregateRootRepository<TAggregateRoot, in TAggregateIdentity>
        where TAggregateRoot : IAggregateRoot<TAggregateIdentity>
        where TAggregateIdentity : IAggregateIdentity
    {
        Task<TAggregateRoot> LoadAsync(TAggregateIdentity aggregateId, int version = int.MaxValue);
        Task SaveAsync(TAggregateRoot aggregate);
    }
}