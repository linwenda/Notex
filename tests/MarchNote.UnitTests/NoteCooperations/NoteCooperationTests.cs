using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarchNote.Domain.NoteCooperations;
using MarchNote.Domain.NoteCooperations.Events;
using MarchNote.Domain.NoteCooperations.Exceptions;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Notes.Exceptions;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Users;
using MarchNote.UnitTests.Notes;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace MarchNote.UnitTests.NoteCooperations
{
    public class NoteCooperationTests : TestBase
    {
        private INoteChecker _noteChecker;
        private NoteCooperation _cooperation;
        private Note _note;
        private Guid _noteAuthorId;

        [SetUp]
        public void SetUp()
        {
            var noteData = NoteTestUtil.CreatePublishedNote();

            _note = noteData.Note;
            _noteAuthorId = noteData.AuthorId;

            _noteChecker = Substitute.For<INoteChecker>();
            _noteChecker.IsAuthorAsync(_note.Id.Value, _noteAuthorId).Returns(true);

            _cooperation = _note.ApplyForWriterAsync(
                Substitute.For<INoteCooperationCounter>(),
                Guid.NewGuid(),
                "test").GetAwaiter().GetResult();
        }

        [Test]
        public async Task Apply_InProgress_ThrowException()
        {
            var cooperationCounter = Substitute.For<INoteCooperationCounter>();
            cooperationCounter.CountPendingAsync(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns(1);

            await Should.ThrowAsync<ApplicationInProgressException>(async () =>
                await _note.ApplyForWriterAsync(cooperationCounter,
                    Guid.NewGuid(),
                    "test"));
        }

        [Test]
        public async Task Approve_ByOtherUser_ThrowException()
        {
            var noteChecker = Substitute.For<INoteChecker>();
            noteChecker.IsAuthorAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(false);

            await Should.ThrowAsync<NotAuthorOfTheNoteException>(async () =>
                await _cooperation.ApproveAsync(noteChecker, Guid.NewGuid()));
        }

        [Test]
        public async Task Approve_ByNoteOwner_IsSuccessful()
        {
            await _cooperation.ApproveAsync(_noteChecker, _noteAuthorId);

            _cooperation.Status.ShouldBe(NoteCooperationStatus.Approved);
            _cooperation.AuditorId.ShouldNotBeNull();
            _cooperation.AuditTime.ShouldNotBeNull();
            _cooperation.DomainEvents.ShouldHaveEvent<NoteCooperationApprovedEvent>();
        }

        [Test]
        public async Task Approve_WithAudited_ThrowException()
        {
            await _cooperation.ApproveAsync(_noteChecker, _noteAuthorId);
            
            await Should.ThrowAsync<InvalidCooperationStatusException>(async () =>
                await _cooperation.ApproveAsync(_noteChecker, _noteAuthorId));
        }

        [Test]
        public async  Task Reject_ByNoteOwner_IsSuccessful()
        {
            await _cooperation.RejectAsync(_noteChecker,_noteAuthorId, "reject");

            var @event = _cooperation.DomainEvents.ShouldHaveEvent<NoteCooperationRejectedEvent>();
            @event.RejectReason.ShouldBe("reject");
        }
    }
}