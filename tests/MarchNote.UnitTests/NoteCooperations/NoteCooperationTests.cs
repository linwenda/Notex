using System;
using System.Collections.Generic;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.NoteCooperations.Events;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;
using MarchNote.UnitTests.Notes;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace MarchNote.UnitTests.NoteCooperations
{
    public class NoteCooperationTests : TestBase
    {
        private NoteCooperation _cooperation;
        private Note _note;
        private UserId _noteAuthorId;
        private NoteMemberGroup _memberList;

        [SetUp]
        public void SetUp()
        {
            var noteData = NoteTestUtil.CreatePublishedNote();
            _note = noteData.Note;
            _noteAuthorId = noteData.AuthorId;

            _cooperation = _note.ApplyForWriter(
                Substitute.For<INoteCooperationCounter>(),
                new UserId(Guid.NewGuid()),
                "test");

            _memberList = new NoteMemberGroup(new List<NoteMember>
            {
                new NoteMember(_noteAuthorId, NoteMemberRole.Owner, DateTime.Now, true, null)
            });
        }

        [Test]
        public void Apply_InProgress_ThrowException()
        {
            var cooperationCounter = Substitute.For<INoteCooperationCounter>();
            cooperationCounter.CountPending(Arg.Any<UserId>(), Arg.Any<NoteId>()).Returns(1);

            ShouldThrowBusinessException(() =>
                    _note.ApplyForWriter(cooperationCounter, new UserId(Guid.NewGuid()), "test"),
                ExceptionCode.CooperationApplicationInProgress);
        }


        [Test]
        public void Approve_ByOtherUser_ThrowException()
        {
           ShouldThrowBusinessException(()=>
               _cooperation.Approve(new UserId(Guid.NewGuid()), _memberList),
               ExceptionCode.CooperationOnlyNoteOwnerCanBeApproved);
        }

        [Test]
        public void Approve_ByNoteOwner_IsSuccessful()
        {
            _cooperation.Approve(_noteAuthorId, _memberList);

            _cooperation.Status.ShouldBe(NoteCooperationStatus.Approved);
            _cooperation.AuditorId.ShouldNotBeNull();
            _cooperation.AuditedAt.ShouldNotBeNull();
            _cooperation.DomainEvents.ShouldHaveEvent<NoteCooperationApprovedEvent>();
        }

        [Test]
        public void Approve_WithAudited_ThrowException()
        {
            _cooperation.Approve(_noteAuthorId, _memberList);

            ShouldThrowBusinessException(() =>
                    _cooperation.Approve(_noteAuthorId, _memberList),
                ExceptionCode.CooperationOnlyPendingCanBeApproved);
        }

        [Test]
        public void Reject_ByNoteOwner_IsSuccessful()
        {
            _cooperation.Reject(_noteAuthorId, _memberList, "reject");

            var @event = _cooperation.DomainEvents.ShouldHaveEvent<NoteCooperationRejectedEvent>();
            @event.RejectReason.ShouldBe("reject");
        }
    }
}