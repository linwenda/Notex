using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.SharedKernel
{
    public class VoteType : ValueObject
    {
        public string Value { get; }
        public static VoteType Neutral => new VoteType(nameof(Neutral));
        public static VoteType Up => new VoteType(nameof(Up));
        public static VoteType Down => new VoteType(nameof(Down));

        private VoteType(string value)
        {
            Value = value;
        }

        public static VoteType Of(string value)
        {
            Guard.Against.InvalidInput(value, nameof(value), v => List.Any(t => t.Value == v));

            return new VoteType(value);
        }

        public static IEnumerable<VoteType> List
        {
            get
            {
                yield return Neutral;
                yield return Up;
                yield return Down;
            }
        }
    }
}