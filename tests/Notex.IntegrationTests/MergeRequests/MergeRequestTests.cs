using System;
using System.Threading.Tasks;
using MediatR;
using Notex.IntegrationTests.Notes;
using Notex.Messages.MergeRequests;
using Notex.Messages.MergeRequests.Commands;
using Notex.Messages.MergeRequests.Queries;
using Notex.Messages.Notes.Commands;
using Xunit;

namespace Notex.IntegrationTests.MergeRequests;

[Collection("Sequence")]
public class MergeRequestTests : IClassFixture<StartupFixture>
{
    private readonly IMediator _mediator;
    private readonly TestHelper _helper;

    public MergeRequestTests(StartupFixture fixture)
    {
        _mediator = fixture.GetService<IMediator>();
        _helper = fixture.GetService<TestHelper>();
    }

    [Fact]
    public async Task CreateMergeRequest_IsSuccessful()
    {
        var noteId = await _helper.CreateDefaultNoteAsync(new NoteOptions());

        var cloneNoteId = await _mediator.Send(new CloneNoteCommand(noteId, Guid.NewGuid()));

        var command = new CreateMergeRequestCommand
        {
            NoteId = cloneNoteId,
            Title = "New Feature",
            Description = "New feature description"
        };

        var mergeRequestId = await _mediator.Send(command);

        var mergeRequest = await _mediator.Send(new GetMergeRequestQuery(mergeRequestId));

        Assert.Equal(command.Title, mergeRequest.Title);
        Assert.Equal(command.Description, mergeRequest.Description);
        Assert.Equal(command.NoteId, mergeRequest.SourceNoteId);
        Assert.Equal(noteId, mergeRequest.DestinationNoteId);
        Assert.Equal(MergeRequestStatus.Open, mergeRequest.Status);
    }

    [Fact]
    public async Task UpdateMergeRequest_IsSuccessful()
    {
        var mergeRequestId = await CreateOpenMergeRequestAsync();

        var command = new UpdateMergeRequestCommand(mergeRequestId, "merge-request title updated",
            "merge-request description updated");

        await _mediator.Send(command);

        var mergeRequest = await _mediator.Send(new GetMergeRequestQuery(mergeRequestId));

        Assert.Equal(command.Title, mergeRequest.Title);
        Assert.Equal(command.Description, mergeRequest.Description);
    }

    [Fact]
    public async Task CloseMergeRequest_IsSuccessful()
    {
        var mergeRequestId = await CreateOpenMergeRequestAsync();

        await _mediator.Send(new CloseMergeRequestCommand(mergeRequestId));

        var mergeRequest = await _mediator.Send(new GetMergeRequestQuery(mergeRequestId));

        Assert.Equal(MergeRequestStatus.Closed, mergeRequest.Status);
    }

    [Fact]
    public async Task ReopenMergeRequest_IsSuccessful()
    {
        var mergeRequestId = await CreateOpenMergeRequestAsync();

        await _mediator.Send(new CloseMergeRequestCommand(mergeRequestId));
        await _mediator.Send(new ReopenMergeRequestCommand(mergeRequestId));

        var mergeRequest = await _mediator.Send(new GetMergeRequestQuery(mergeRequestId));

        Assert.Equal(MergeRequestStatus.Open, mergeRequest.Status);
    }

    private async Task<Guid> CreateOpenMergeRequestAsync()
    {
        var noteId = await _helper.CreateDefaultNoteAsync(new NoteOptions());

        var cloneNoteId = await _mediator.Send(new CloneNoteCommand(noteId, Guid.NewGuid()));

        var command = new CreateMergeRequestCommand
        {
            NoteId = cloneNoteId,
            Title = "New Feature",
            Description = "New feature description"
        };

        return await _mediator.Send(command);
    }
}