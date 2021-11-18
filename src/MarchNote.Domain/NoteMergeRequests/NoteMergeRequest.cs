using System;
using System.Threading.Tasks;
using MarchNote.Domain.NoteMergeRequests.Events;
using MarchNote.Domain.NoteMergeRequests.Exceptions;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.NoteMergeRequests
{
    public sealed class NoteMergeRequest : Entity<Guid>, IHasCreationTime
    {
        public Guid CreatorId { get; private set; }
        public DateTime CreationTime { get; private set; }
        public Guid NoteId { get; private set; }
        public Guid? ReviewerId { get; private set; }
        public DateTime? ReviewTime { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public NoteMergeRequestStatus Status { get; private set; }

        internal NoteMergeRequest(Guid noteId, string title, string description)
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.UtcNow;
            NoteId = noteId;
            Title = title;
            Description = description;
            Status = NoteMergeRequestStatus.Open;
        }

        public async Task CloseAsync(INoteChecker noteChecker, Guid userId)
        {
            var isAuthor = await noteChecker.IsAuthorAsync(NoteId, userId);

            if (!isAuthor && CreatorId != userId)
            {
                throw new OnlyNoteAuthorOrCreatorCanBeClosedException();
            }

            Status = NoteMergeRequestStatus.Closed;

            if (isAuthor)
            {
                ReviewerId = userId;
                ReviewTime = DateTime.UtcNow;
            }

            AddDomainEvent(new NoteMergeRequestClosedEvent(Id));
        }

        public async Task MergeAsync(INoteChecker noteChecker, Guid userId)
        {
            if (!await noteChecker.IsAuthorAsync(NoteId, userId))
            {
                throw new OnlyNoteAuthorCanBeMergedException();
            }

            ReviewerId = userId;
            ReviewTime = DateTime.UtcNow;
            Status = NoteMergeRequestStatus.Merged;

            AddDomainEvent(new NoteMergeRequestMergedEvent(Id, NoteId));
        }
    }
}