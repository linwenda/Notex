using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;

namespace Funzone.Domain.Posts
{
    public class PostReview : Entity
    {
        public int Id { get; private set; }
        public PostId PostId { get; private set; }
        public PostStatus PostStatus { get; private set; }
        public UserId ReviewerId { get; private set; }
        public string Comment { get; private set; }
        public DateTime ReviewedTime { get; private set; }

        private PostReview()
        {

        }

        public PostReview(PostId postId, PostStatus postStatus, UserId reviewerId, string comment = "")
        {
            PostId = postId;
            PostStatus = postStatus;
            ReviewerId = reviewerId;
            Comment = comment;
            ReviewedTime = Clock.Now;
        }
    }
}