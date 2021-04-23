using Funzone.Domain.SeedWork;

namespace Funzone.Domain.SharedKernel
{
    public class VoteType : ValueObject
    {
        public string Value { get; }
        public static VoteType Up => new VoteType(nameof(Up));
        public static VoteType Down => new VoteType(nameof(Down));
        public static VoteType Neutral => new VoteType(nameof(Neutral));

        private VoteType(string value)
        {
            Value = value;
        }

        private VoteType()
        {
        }
    }
}