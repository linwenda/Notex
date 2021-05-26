using System;

namespace Funzone.Domain.SeedWork
{
    public abstract class AggregateId<T>
        where T : AggregateRoot
    {
        protected AggregateId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }
    }
}
