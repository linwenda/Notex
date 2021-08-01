using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using MarchNote.Domain;
using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.NoteAggregate.Events;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;

namespace MarchNote.UnitTests.Notes
{
    public class NoteTests : TestBase
    {
        private Note _note;
        private UserId _userId;

        [SetUp]
        public void SetUp()
        {
            var space = Space.Create(new UserId(Guid.NewGuid()), "space", "#FFF", "Bear");

            _userId = space.AuthorId;
            _note = new Note(new NoteId(Guid.NewGuid()));
            _note.Create(
                space,
                _userId,
                "Asp.NET Core",
                "About ASP.NET Core");
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
            ShouldThrowBusinessException(() =>
                    _note.Edit(new UserId(Guid.NewGuid()), "Asp.NET Core 3.1", ""),
                ExceptionCode.NotePermissionDenied);
        }

        [Test]
        public void Edit_WasDeleted_ThrowException()
        {
            _note.Delete(_userId);

            ShouldThrowBusinessException(() =>
                    _note.Edit(_userId, "Asp.NET Core 3.1", ""),
                ExceptionCode.NoteHasBeenDeleted);
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
            noteSnapshot.AuthorId.ShouldBe(_userId.Value);
            noteSnapshot.Content.ShouldBe("");
            noteSnapshot.Status.ShouldBe(NoteStatus.Published);
            noteSnapshot.MemberList
                .SingleOrDefault(m => m.MemberId == _userId.Value && m.Role == NoteMemberRole.Owner.Value)
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
            var inviteUserId = new UserId(Guid.NewGuid());

            _note.InviteUser(_userId, inviteUserId, NoteMemberRole.Writer);

            var @event = _note.GetUnCommittedEvents().ShouldHaveEvent<NoteMemberInvitedEvent>();
            @event.Role.ShouldBe(NoteMemberRole.Writer.Value);
            @event.MemberId.ShouldBe(inviteUserId.Value);
            @event.NoteId.ShouldBe(_note.Id.Value);
        }

        [Test]
        public void InviteUser_WasJoined_ThrowException()
        {
            var inviteUserId = new UserId(Guid.NewGuid());
            _note.InviteUser(_userId, inviteUserId, NoteMemberRole.Writer);

            ShouldThrowBusinessException(() => _note.InviteUser(_userId, inviteUserId, NoteMemberRole.Writer),
                ExceptionCode.NoteUserHasBeenJoined);
        }

        [Test]
        public void RemoveUser_IsSuccessful()
        {
            var inviteUserId = new UserId(Guid.NewGuid());

            _note.InviteUser(_userId, inviteUserId, NoteMemberRole.Writer);
            _note.RemoveMember(_userId, inviteUserId);
            var @event = _note.GetUnCommittedEvents().ShouldHaveEvent<NoteMemberRemovedEvent>();
            @event.MemberId.ShouldBe(inviteUserId.Value);
        }

        [Test]
        public void RemoveUser_WasRemoved_ThrowException()
        {
            var memberId = new UserId(Guid.NewGuid());
            _note.InviteUser(_userId, memberId, NoteMemberRole.Writer);
            _note.RemoveMember(_userId, memberId);

            ShouldThrowBusinessException(() => _note.RemoveMember(_userId, memberId),
                ExceptionCode.NoteMemberHasBeenRemoved);
        }
    }
}