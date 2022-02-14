using SmartNote.Core.Ddd;
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
            var noteSnapshot = snapshot as NoteSnapshot ?? throw new EventSourcedAggregateRootException("Invalid snapshot");

            _authorId = noteSnapshot.AuthorId;
            _forkId = noteSnapshot.FromId.HasValue ? new NoteId(noteSnapshot.FromId.Value) : null;
            _isDeleted = noteSnapshot.IsDeleted;
            _title = noteSnapshot.Title;
            _content = noteSnapshot.Content;
            _status = noteSnapshot.Status;
        }

        protected override ISnapshot<NoteId> CreateSnapshot()
        {
            return new NoteSnapshot(
                Id,
                Version,
                _forkId?.Value,
                _authorId,
                _title,
                _content,
                _isDeleted,
                _status);
        }

        private void When(NoteCreatedEvent @event)
        {
            _authorId = @event.AuthorId;
            _spaceId = @event.SpaceId;
            _title = @event.Title;
            _isDeleted = false;
            _status = NoteStatus.Draft;
            _content = new List<Block>();
        }

        private void When(NoteForkedEvent @event)
        {
            _authorId = @event.AuthorId;
            _spaceId = @event.SpaceId;
            _forkId = new NoteId(@event.FromNoteId);
            _title = @event.Title;
            _content = @event.Content;
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
            _content = @event.Content;
        }
    }
}