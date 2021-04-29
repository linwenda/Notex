using System;
using Funzone.Domain.PostComments;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;

namespace Funzone.Domain.PostCommentVotes
{
    public class PostCommentVote : Entity, IAggregateRoot
    {
        public PostCommentVoteId Id { get; private set; }
        public PostCommentId PostCommentId { get; private set; }
        public DateTime VotedTime { get; private set; }
        public UserId VoterId { get; private set; }
        public VoteType VoteType { get; private set; }

        private PostCommentVote()
        {
            //Only for EF
        }

        public PostCommentVote(PostCommentId postCommentId, UserId voterId, VoteType voteType)
        {
            PostCommentId = postCommentId;
            VoterId = voterId;
            VoteType = voteType;

            Id = new PostCommentVoteId(Guid.NewGuid());
            VotedTime = SystemClock.Now;
        }

        public void Up(UserId userId)
        {
            CheckRule(new PostCommentCanBeVotedOnlyByVoterRule(VoterId, userId));
            VoteType = VoteType.Up;
        }

        public void Down(UserId userId)
        {
            CheckRule(new PostCommentCanBeVotedOnlyByVoterRule(VoterId, userId));
            VoteType = VoteType.Down;
        }

        public void Neutralize(UserId userId)
        {
            CheckRule(new PostCommentCanBeVotedOnlyByVoterRule(VoterId, userId));
            VoteType = VoteType.Neutral;
        }
    }
}