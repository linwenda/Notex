﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Notes.Events;
using MarchNote.Domain.Notes.Exceptions;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;
using MarchNote.UnitTests.Spaces;
using NSubstitute;

namespace MarchNote.UnitTests.Notes
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
                "Asp.NET Core",
                "About ASP.NET Core",
                new List<string>());
            _note.Publish(_userId);
        }

        [Test]
        public void Edit_ByAuthor_IsSuccessful()
        {
            _note.Edit(_userId, "Asp.NET Core 3.1", "");

            var @event = _note.GetUnCommittedEvents().ShouldHaveEvent<NoteEditedEvent>();

            @event.Title.ShouldBe("Asp.NET Core 3.1");
            @event.Content.ShouldBe("");
        }

        [Test]
        public void Edit_NoAuthor_ThrowException()
        {
            Should.Throw<NotAuthorOfTheNoteException>(() => _note.Edit(Guid.NewGuid(), 
                "Asp.NET Core 3.1", ""));
        }

        [Test]
        public void Edit_WasDeleted_ThrowException()
        {
            _note.Delete(_userId);
            Should.Throw<NoteHasBeenDeletedException>(() =>_note.Delete(_userId));
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
            _note.Edit(_userId, "Asp.NET Core 3.1", "");
            _note.Publish(_userId);
            _note.TakeSnapshot();

            var snapshot = _note.GetUnCommittedSnapshot();

            var noteSnapshot = snapshot as NoteSnapshot;
            noteSnapshot.ShouldNotBeNull();
            noteSnapshot.AuthorId.ShouldBe(_userId);
            noteSnapshot.Content.ShouldBe("");
            noteSnapshot.Status.ShouldBe(NoteStatus.Published);
            noteSnapshot.MemberList
                .SingleOrDefault(m => m.MemberId == _userId && m.Role == NoteMemberRole.Owner.Value)
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