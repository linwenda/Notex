﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarchNote.Domain.NoteComments;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.Notes.Events;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.SeedWork.EventSourcing;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.Notes
{
    public partial class Note : EventSourcedEntity<NoteId>
    {
        private NoteId _fromId;
        private UserId _authorId;
        private SpaceId _spaceId;
        private string _title;
        private string _content;
        private bool _isDeleted;
        private NoteStatus _status;
        private NoteMemberGroup _memberGroup;
        private List<string> _tags;

        private Note(NoteId id) : base(id)
        {
        }

        public static Note Create(
            Space space,
            UserId userId,
            string title,
            string content,
            List<string> tags)
        {
            var note = new Note(new NoteId(Guid.NewGuid()));

            space.CheckDelete();
            space.CheckAuthor(userId, "Only space author can add note");

            note.ApplyChange(new NoteCreatedEvent(
                note.Id.Value,
                space.Id.Value,
                userId.Value,
                DateTime.UtcNow,
                title,
                content,
                NoteStatus.Draft,
                tags ?? new List<string>()));

            return note;
        }

        public Note DraftOut(UserId userId)
        {
            CheckPublished();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner, NoteMemberRole.Writer);

            var note = new Note(new NoteId(Guid.NewGuid()));

            note.ApplyChange(new NoteDraftedOutEvent(
                note.Id.Value,
                Id.Value,
                userId.Value,
                _spaceId.Value,
                DateTime.UtcNow,
                _title,
                _content,
                _tags));

            return note;
        }

        public void Edit(
            UserId userId,
            string title,
            string content)
        {
            CheckDelete();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            ApplyChange(new NoteEditedEvent(
                Id.Value,
                title,
                content,
                _status));
        }

        public void Publish(UserId userId)
        {
            CheckDelete();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (_fromId != null)
            {
                throw new NoteException("Only main note can be published");
            }

            if (_status != NoteStatus.Published)
            {
                ApplyChange(new NotePublishedEvent(
                    Id.Value,
                    DateTime.UtcNow,
                    NoteStatus.Published));
            }
        }

        public void Merge(UserId userId)
        {
            CheckDelete();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (_fromId == null)
            {
                throw new NoteException("Only draft out note can be merged");
            }

            ApplyChange(new NoteMergedEvent(
                Id.Value,
                _fromId.Value,
                userId.Value,
                _title,
                _content,
                _tags));
        }

        public void Update(UserId userId, string title, string content, List<string> tags)
        {
            CheckPublished();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner, NoteMemberRole.Writer);

            ApplyChange(new NoteUpdatedEvent(Id.Value, title, content, tags));
        }

        public void Delete(UserId userId)
        {
            CheckDelete();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            ApplyChange(new NoteDeletedEvent(Id.Value));
        }

        public void InviteUser(UserId userId, UserId inviteUserId, NoteMemberRole role)
        {
            CheckPublished();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (_memberGroup.IsMember(inviteUserId))
            {
                throw new NoteException("User has been joined");
            }

            ApplyChange(new NoteMemberInvitedEvent(
                Id.Value,
                inviteUserId.Value,
                role.Value,
                DateTime.UtcNow));
        }

        public void RemoveMember(UserId userId, UserId removeUserId)
        {
            CheckPublished();
            CheckAtLeastOneRole(userId, NoteMemberRole.Owner);

            if (!_memberGroup.IsMember(removeUserId))
            {
                throw new NoteException("Member has been removed");
            }

            ApplyChange(new NoteMemberRemovedEvent(
                Id.Value,
                removeUserId.Value,
                DateTime.UtcNow));
        }

        public async Task<NoteCooperation> ApplyForWriterAsync(
            INoteCooperationCounter cooperationCounter,
            UserId userId,
            string comment)
        {
            CheckPublished();

            if (_memberGroup.IsWriter(userId))
            {
                throw new NoteException("You already is the writer of the note");
            }

            return await NoteCooperation.ApplyAsync(
                cooperationCounter,
                Id,
                userId,
                comment);
        }

        public NoteComment AddComment(UserId userId, string comment)
        {
            CheckPublished();

            return NoteComment.Create(Id, userId, comment);
        }

        private void CheckDelete()
        {
            if (_isDeleted)
            {
                throw new NoteException("Note has been deleted");
            }
        }

        private void CheckAtLeastOneRole(UserId userId, params NoteMemberRole[] roles)
        {
            if (!roles.Any(r => _memberGroup.InRole(userId, r)))
            {
                throw new NoteException("Permission denied");
            }
        }

        private void CheckPublished()
        {
            CheckDelete();
            if (_status != NoteStatus.Published)
            {
                throw new NoteException("Only published note can be operated");
            }
        }
    }
}