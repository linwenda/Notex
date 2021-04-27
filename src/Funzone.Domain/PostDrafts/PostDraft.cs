using System;
using Funzone.Domain.PostDrafts.Rules;
using Funzone.Domain.Posts;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;

namespace Funzone.Domain.PostDrafts
{
    public class PostDraft : Entity, IAggregateRoot
    {
        public PostDraftId Id { get; private set; }
        public ZoneId ZoneId { get; private set; }
        public UserId AuthorId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public bool IsPosted { get; private set; }

        public PostDraft(ZoneMember member, string title, string content)
        {
            ZoneId = member.ZoneId;
            AuthorId = member.UserId;
            Title = title;
            Content = content;

            Id = new PostDraftId(Guid.NewGuid());
            CreatedTime = Clock.Now;
        }

        public Post Post(ZoneMember postingMember)
        {
            CheckRule(new PostDraftCanBePostedOnlyByAuthorRule(AuthorId, postingMember.UserId));
            IsPosted = true;
            return new Post(postingMember, Title, Content, PostType.Text);
        }

        public void Edit(UserId editorId, string title, string content)
        {
            CheckRule(new PostDraftCanBeEditedOnlyByAuthorRule(AuthorId, editorId));
            Title = title;
            Content = content;
        }
    }
}