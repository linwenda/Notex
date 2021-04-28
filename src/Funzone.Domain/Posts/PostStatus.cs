using System.Collections.Generic;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Posts
{
    public class PostStatus : ValueObject
    {
        public static PostStatus WaitingForReview => new PostStatus(nameof(WaitingForReview));
        public static PostStatus Approved => new PostStatus(nameof(Approved));
        public static PostStatus Rejected => new PostStatus(nameof(Rejected));
        public static PostStatus RePost => new PostStatus(nameof(RePost));

        public string Value { get; }

        public PostStatus(string value)
        {
            Value = value;
        }

        public static IEnumerable<PostStatus> List
        {
            get
            {
                yield return WaitingForReview;
                yield return Approved;
                yield return Rejected;
                yield return RePost;
            }
        }
    }
}