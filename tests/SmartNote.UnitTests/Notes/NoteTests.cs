using System;
using System.Collections.Generic;
using System.Linq;
using MarchNote.UnitTests;
using NUnit.Framework;
using Shouldly;
using SmartNote.Domain.Notes;
using SmartNote.Domain.Notes.Blocks;
using SmartNote.Domain.Notes.Events;
using SmartNote.Domain.Notes.Exceptions;
using SmartNote.UnitTests.Spaces;

namespace SmartNote.UnitTests.Notes
{
    public class NoteTests : TestBase
    {
        private Note _note;
        private Guid _userId;

        [SetUp]
        public void SetUp()
        {
            var space = SpaceTestUtil.CreateSpace();

            _userId = space.AuthorId;
            _note = space.CreateNote(
                _userId,
                "Asp.NET Core");
            _note.Publish(_userId);
        }

        [Test]
        public void Edit_WasDeleted_ThrowException()
        {
            _note.Delete(_userId);
            Should.Throw<NoteHasBeenDeletedException>(() => _note.Delete(_userId));
        }

        [Test]
        public void Publish_IsSuccessful()
        {
            _note.Publish(_userId);
            var @event = _note.GetUnCommittedEvents().ShouldHaveEvent<NotePublishedEvent>();
            @event.Status.ShouldBe(NoteStatus.Published);
        }

        [Test]
        public void TakeSnapshot()
        {
            _note.Update(_userId, new List<Block>());
            _note.Publish(_userId);
            _note.TakeSnapshot();

            var snapshot = _note.GetUnCommittedSnapshot();

            var noteSnapshot = snapshot as NoteSnapshot;
            noteSnapshot.ShouldNotBeNull();
            noteSnapshot.AuthorId.ShouldBe(_userId);
            noteSnapshot.Status.ShouldBe(NoteStatus.Published);
            noteSnapshot.Blocks.ShouldBeEmpty();
            noteSnapshot.MemberList
                .SingleOrDefault(m => m.MemberId == _userId && m.Role == NoteMemberRole.Author.Value)
                .ShouldNotBeNull();
        }

        [Test]
        public void Delete_IsSuccessful()
        {
            _note.Delete(_userId);
            _note.GetUnCommittedEvents()
                .ShouldHaveEvent<NoteDeletedEvent>();
        }

        [Test]
        public void InviteUser_IsSuccessful()
        {
            var inviteUserId = Guid.NewGuid();

            _note.InviteUser(_userId, inviteUserId, NoteMemberRole.Writer);

            var @event = _note.GetUnCommittedEvents().ShouldHaveEvent<NoteMemberInvitedEvent>();
            @event.Role.ShouldBe(NoteMemberRole.Writer.Value);
            @event.MemberId.ShouldBe(inviteUserId);
            @event.NoteId.ShouldBe(_note.Id.Value);
        }

        [Test]
        public void InviteUser_WasJoined_ThrowException()
        {
            var inviteUserId = Guid.NewGuid();
            _note.InviteUser(_userId, inviteUserId, NoteMemberRole.Writer);

            Should.Throw<UserHasBeenJoinedThisNoteCooperationException>(() =>
                _note.InviteUser(_userId, inviteUserId, NoteMemberRole.Writer));
        }

        [Test]
        public void RemoveUser_IsSuccessful()
        {
            var inviteUserId = Guid.NewGuid();

            _note.InviteUser(_userId, inviteUserId, NoteMemberRole.Writer);
            _note.RemoveMember(_userId, inviteUserId);
            var @event = _note.GetUnCommittedEvents().ShouldHaveEvent<NoteMemberRemovedEvent>();
            @event.MemberId.ShouldBe(inviteUserId);
        }
    }
}