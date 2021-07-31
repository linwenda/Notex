using System;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Responses;
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
                "I want to edit the note.");

            var applyResponse = await Send(applyCommand);
            applyResponse.Code.ShouldBe(DefaultResponseCode.Succeeded);

            var queryResponse = await Send(new GetNoteCooperationByIdQuery(applyResponse.Data));
            queryResponse.Data.Comment.ShouldBe(applyCommand.Comment);
            queryResponse.Data.Status.ShouldBe(NoteCooperationStatus.Pending);
            queryResponse.Data.NoteId.ShouldBe(noteId);
        }

        [Test]
        public async Task ShouldApproveCooperation()
        {
            var cooperationId = await CreateCooperation();

            await Send(new ApproveNoteCooperationCommand(cooperationId));
            
            var queryResponse = await Send(new GetNoteCooperationByIdQuery(cooperationId));
            queryResponse.Data.Status.ShouldBe(NoteCooperationStatus.Approved);
            queryResponse.Data.AuditedAt.ShouldNotBeNull();
        }
        
        [Test]
        public async Task ShouldRejectCooperation()
        {
            var cooperationId = await CreateCooperation();

            await Send(new RejectNoteCooperationCommand(cooperationId,"reject"));
            
            var queryResponse = await Send(new GetNoteCooperationByIdQuery(cooperationId));
            queryResponse.Data.Status.ShouldBe(NoteCooperationStatus.Rejected);
            queryResponse.Data.AuditedAt.ShouldNotBeNull();
            queryResponse.Data.RejectReason.ShouldBe("reject");
        }

        private async Task<Guid> CreateCooperation()
        {
            var noteId = await NoteTestUtil.CreatePublishedNote();

            var applyCommand = new ApplyForNoteCooperationCommand(noteId,
                "I want to edit the note.");
            
            var applyResponse = await Send(applyCommand);
            return applyResponse.Data;
        }
    }
}