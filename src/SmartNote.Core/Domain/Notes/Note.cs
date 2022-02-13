﻿using SmartNote.Core.Domain.NoteComments;
using SmartNote.Core.Domain.Notes.Blocks;
using SmartNote.Core.Domain.Notes.Events;
using SmartNote.Core.Domain.Notes.Exceptions;

namespace SmartNote.Core.Domain.Notes
{
    public partial class Note : EventSourcedAggregateRoot<NoteId>
    {
        private NoteId _forkId;
        private Guid _authorId;
        private Guid _spaceId;
        private string _title;
        private bool _isDeleted;
        private NoteStatus _status;
        private List<Block> _blocks;
        private List<string> _tags;

        private Note(NoteId id) : base(id)
        {
        }

        internal static Note Create(
            Guid spaceId,
            Guid userId,
            string title)
        {
            var note = new Note(new NoteId(Guid.NewGuid()));
            note.ApplyChange(new NoteCreatedEvent(
                note.Id.Value,
                spaceId,
                userId,
                DateTime.UtcNow,
                title,
                NoteStatus.Draft));

            return note;
        }

        public Note Fork(Guid userId)
        {
            CheckDeleted();
            CheckAuthor(userId);
            CheckNoteStatus(NoteStatus.Published, "Only published note can be forked");

            var note = new Note(new NoteId(Guid.NewGuid()));

            note.ApplyChange(new NoteForkedEvent(
                note.Id.Value,
                Id.Value,
                userId,
                _spaceId,
                DateTime.UtcNow,
                _title,
                _blocks,
                _tags));

            return note;
        }

        public void Publish(Guid userId)
        {
            CheckDeleted();
            CheckAuthor(userId);

            if (_status != NoteStatus.Published)
            {
                ApplyChange(new NotePublishedEvent(
                    Id.Value,
                    DateTime.UtcNow,
                    NoteStatus.Published));
            }
        }

        public void Merge(
            Guid fromNoteId,
            Guid userId,
            string title,
            List<Block> blocks,
            List<string> tags)
        {
            CheckDeleted();
            CheckAuthor(userId);

            if (_forkId == null)
            {
                throw new OnlyForkNoteCanBeMergedException();
            }

            ApplyChange(new NoteMergedEvent(
                fromNoteId,
                Id.Value,
                userId,
                title,
                blocks,
                tags));
        }

        public void Update(
            Guid userId,
            List<Block> blocks)
        {
            CheckAuthor(userId);
            
            ApplyChange(new NoteUpdatedEvent(Id.Value, blocks ?? new List<Block>()));
        }

        public void Delete(Guid userId)
        {
            CheckDeleted();
            CheckAuthor(userId);

            ApplyChange(new NoteDeletedEvent(Id.Value));
        }

        public NoteId GetForkId()
        {
            return _forkId;
        }

        public NoteSnapshot GetSnapshot()
        {
            Guid? formId = null;

            if (_forkId != null)
            {
                formId = _forkId.Value;
            }

            return new NoteSnapshot(Id,
                Version,
                formId,
                _authorId,
                _title,
                _blocks,
                _isDeleted,
                _status);
        }


        public NoteComment AddComment(Guid userId, string comment)
        {
            CheckNoteStatus(NoteStatus.Published);

            return NoteComment.Create(Id.Value, userId, comment);
        }

        private void CheckAuthor(Guid userId)
        {
            if (_authorId != userId)
            {
                throw new NotAuthorOfTheNoteException();
            }
        }

        private void CheckDeleted()
        {
            if (_isDeleted)
            {
                throw new NoteHasBeenDeletedException();
            }
        }

        private void CheckNoteStatus(NoteStatus status, string errorMessage = "Invalid note status")
        {
            if (_status != status)
            {
                throw new InvalidNoteStatusException(errorMessage);
            }
        }
    }
}