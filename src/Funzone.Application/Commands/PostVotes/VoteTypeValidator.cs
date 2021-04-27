using System.Linq;
using Funzone.Domain.SharedKernel;

namespace Funzone.Application.Commands.PostVotes
{
    public static class VoteTypeValidator
    {
        public static bool IsSupportType(string voteType)
        {
            return !string.IsNullOrEmpty(voteType) && VoteType.SupportTypes.Any(t => t.Value == voteType);
        }
    }
}
