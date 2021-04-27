using System;
using Funzone.Domain.Posts;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using JetBrains.Annotations;

namespace Funzone.Domain.PostComments
{
    public class PostComment : Entity, IAggregateRoot
    {
        public PostCommentId Id { get; private set; }
        public PostId PostId { get; private set; }
        public UserId AuthorId { get; private set; }
        [CanBeNull] public PostCommentId ReplayToCommentId { get; private set; }
        public string Comment { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public DateTime? EditedTime { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsRemoved { get; set; }
        public string RemovedReason { get; private set; }

        private PostComment()
        {
        }

        public PostComment(
            PostId postId,
            UserId authorId,
            PostCommentId replayToCommentId,
            string comment)
        {
            PostId = postId;
            AuthorId = authorId;
            ReplayToCommentId = replayToCommentId;
            Comment = comment;

            Id = new PostCommentId(Guid.NewGuid());
            CreatedTime = Clock.Now;
        }

        public void Edit(UserId editorId, string comment)
        {
            Comment = comment;
            EditedTime = DateTime.UtcNow;
        }

        public void Delete(UserId deleterId)
        {
            IsDeleted = true;
        }

        public void Remove(UserId removingUserId, string removeComment)
        {
            IsRemoved = true;
            RemovedReason = removeComment;
        }

        public void Replay()
        {
        }
    }
}