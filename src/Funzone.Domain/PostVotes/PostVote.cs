using Funzone.Domain.Posts;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using System;

namespace Funzone.Domain.PostVotes
{
    public class PostVote : Entity, IAggregateRoot
    {
        public PostVoteId Id { get; private set; }
        public PostId PostId { get; private set; }
        public DateTime VotedTime { get; private set; }
        public UserId VoterId { get; private set; }
        public VoteType VoteType { get; private set; }

        private PostVote()
        {
            //Only for EF
        }

        public PostVote(
            PostId postId,
            UserId voterId,
            VoteType voteType)
        {
            PostId = postId;
            VoterId = voterId;
            VoteType = voteType;

            Id = new PostVoteId(Guid.NewGuid());
            VotedTime = Clock.Now;
        }

        public void ReVote(UserId userId,VoteType voteType)
        {
            CheckRule(new PostCanBeReVotedOnlyByVoterRule(VoterId, userId));
            VoteType = voteType;
        }
    }
}