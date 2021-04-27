using Funzone.Domain.SeedWork;

namespace Funzone.Domain.SharedKernel
{
    public class VoteType : Enumeration
    {
        public static VoteType Neutral => new VoteType(0, nameof(Neutral));

        public static VoteType Up => new VoteType(1, nameof(Up));

        public static VoteType Down => new VoteType(2, nameof(Down));

        private VoteType(int id, string name) : base(id, name)
        {
        }
    }
}