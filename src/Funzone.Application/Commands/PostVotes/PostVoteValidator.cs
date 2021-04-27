using System.Linq;
using Funzone.Domain.SharedKernel;

namespace Funzone.Application.Commands.PostVotes
{
    public static class PostVoteValidator
    {
        public static bool IsSupportVoteType(string voteType)
        {
            return !string.IsNullOrEmpty(voteType) && VoteType.List.Any(t => t.Value == voteType);
        }
    }
}
