using SmartNote.Domain.Notes.Blocks;
using SmartNote.Domain.Notes.Events;

namespace SmartNote.Domain.Notes
{
    public partial class Note
    {
        protected override void Apply(IDomainEvent @event)
        {
            this.When((dynamic)@event);
        }

        protected override void LoadSnapshot(ISnapshot snapshot)
        {
            var noteSnapshot = snapshot as NoteSnapshot ?? throw new AggregateRootException("Invalid snapshot");

            _authorId = noteSnapshot.AuthorId;
            _forkId = noteSnapshot.FromId.HasValue ? new NoteId(noteSnapshot.FromId.Value) : null;
            _isDeleted = noteSnapshot.IsDeleted;
            _title = noteSnapshot.Title;
            _blocks = noteSnapshot.Blocks;
            _status = noteSnapshot.Status;
            _memberGroup = new NoteMemberGroup(noteSnapshot.MemberList.Select(m => m.ToMember()).ToList());
        }

        protected override ISnapshot CreateSnapshot()
        {
            return GetSnapshot();
        }

        private void When(NoteCreatedEvent @event)
        {
            _authorId = @event.AuthorId;
            _spaceId = @event.SpaceId;
            _title = @event.Title;
            _isDeleted = false;
            _status = NoteStatus.Draft;
            _blocks = new List<Block>();

            _memberGroup = new NoteMemberGroup(new List<NoteMember>());
            _memberGroup.AddMember(_authorId, NoteMemberRole.Author);
        }

        private void When(NoteForkedEvent @event)
        {
            _authorId = @event.AuthorId;
            _spaceId = @event.SpaceId;
            _forkId = new NoteId(@event.FromNoteId);
            _title = @event.Title;
            _blocks = @event.Blocks;
            _isDeleted = false;
            _status = NoteStatus.Draft;
            _tags = @event.Tags;

            _memberGroup = new NoteMemberGroup(new List<NoteMember>());
            _memberGroup.AddMember(_authorId, NoteMemberRole.Author);
        }

        private void When(NoteDeletedEvent @event)
        {
            _isDeleted = true;
        }

        private void When(NotePublishedEvent @event)
        {
            _status = NoteStatus.Published;
        }

        private void When(NoteMergedEvent @event)
        {
            _isDeleted = true;
        }

        private void When(NoteUpdatedEvent @event)
        {
            _blocks = @event.Blocks;
        }

        private void When(NoteMemberInvitedEvent @event)
        {
            _memberGroup.AddMember(@event.MemberId, NoteMemberRole.Of(@event.Role));
        }

        private void When(NoteMemberRemovedEvent @event)
        {
            _memberGroup.RemoveMember(@event.MemberId);
        }
    }
}