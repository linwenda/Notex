using System;
using System.Threading.Tasks;
using MarchNote.Application.NoteCooperations.Commands;
using MarchNote.Application.NoteCooperations.Queries;
using MarchNote.Domain.NoteCooperations;
using NUnit.Framework;
using Shouldly;

namespace MarchNote.IntegrationTests.Notes
{
    using static TestFixture;

    public class NoteCooperationTests : TestBase
    {
        [Test]
        public async Task ShouldApplyForCooperation()
        {
            var noteId = await NoteTestUtil.CreatePublishedNote();

            var applyCommand = new ApplyForNoteCooperationCommand(noteId,
                "I want to edit this note.");

            var applyResponse = await Send(applyCommand);

            var queryResponse = await Send(new GetNoteCooperationByIdQuery(applyResponse));
            queryResponse.Comment.ShouldBe(applyCommand.Comment);
            queryResponse.Status.ShouldBe(NoteCooperationStatus.Pending);
            queryResponse.NoteId.ShouldBe(noteId);
        }

        [Test]
        public async Task ShouldApproveCooperation()
        {
            var cooperationId = await CreateCooperation();

            await Send(new ApproveNoteCooperationCommand(cooperationId));
            
            var queryResponse = await Send(new GetNoteCooperationByIdQuery(cooperationId));
            queryResponse.Status.ShouldBe(NoteCooperationStatus.Approved);
            queryResponse.AuditTime.ShouldNotBeNull();
        }
        
        [Test]
        public async Task ShouldRejectCooperation()
        {
            var cooperationId = await CreateCooperation();

            await Send(new RejectNoteCooperationCommand(cooperationId,"reject"));
            
            var queryResponse = await Send(new GetNoteCooperationByIdQuery(cooperationId));
            queryResponse.Status.ShouldBe(NoteCooperationStatus.Rejected);
            queryResponse.AuditTime.ShouldNotBeNull();
            queryResponse.RejectReason.ShouldBe("reject");
        }

        private async Task<Guid> CreateCooperation()
        {
            var noteId = await NoteTestUtil.CreatePublishedNote();

            var applyCommand = new ApplyForNoteCooperationCommand(noteId,
                "I want to edit the note.");
            
            var applyResponse = await Send(applyCommand);
            return applyResponse;
        }
    }
}