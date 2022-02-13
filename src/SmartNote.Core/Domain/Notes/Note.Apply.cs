using SmartNote.Core.Domain.Notes.Blocks;
using SmartNote.Core.Domain.Notes.Events;

namespace SmartNote.Core.Domain.Notes
{
    public partial class Note
    {
        protected override void Apply(IDomainEvent @event)
        {
            this.When((dynamic)@event);
        }

        protected override void LoadSnapshot(ISnapshot snapshot)
        {
            var noteSnapshot = snapshot as NoteSnapshot ?? throw new EventSourcedAggregateRootException("Invalid snapshot");

            _authorId = noteSnapshot.AuthorId;
            _forkId = noteSnapshot.FromId.HasValue ? new NoteId(noteSnapshot.FromId.Value) : null;
            _isDeleted = noteSnapshot.IsDeleted;
            _title = noteSnapshot.Title;
            _blocks = noteSnapshot.Blocks;
            _status = noteSnapshot.Status;
        }

        protected override ISnapshot<NoteId> CreateSnapshot()
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
    }
}