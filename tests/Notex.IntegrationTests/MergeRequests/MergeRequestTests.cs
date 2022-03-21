using System;
using System.Threading.Tasks;
using Notex.Core.Queries;
using Notex.IntegrationTests.Notes;
using Notex.Messages.MergeRequests;
using Notex.Messages.MergeRequests.Commands;
using Notex.Messages.Notes.Commands;
using Xunit;

namespace Notex.IntegrationTests.MergeRequests;

[Collection(IntegrationCollection.Application)]
public class MergeRequestTests : IClassFixture<IntegrationFixture>
{
    private readonly IntegrationFixture _fixture;
    private readonly IMergeRequestQuery _mergeRequestQuery;

    public MergeRequestTests(IntegrationFixture fixture)
    {
        _fixture = fixture;
        _mergeRequestQuery = fixture.GetService<IMergeRequestQuery>();
    }

    [Fact]
    public async Task CreateMergeRequest_IsSuccessful()
    {
        var noteId = await _fixture.CreateDefaultNoteAsync(new NoteOptions());

        var cloneNoteId = await _fixture.Mediator.Send(new CloneNoteCommand(noteId, Guid.NewGuid()));

        var command = new CreateMergeRequestCommand
        {
            NoteId = cloneNoteId,
            Title = "New Feature",
            Description = "New feature description"
        };

        var mergeRequestId = await _fixture.Mediator.Send(command);

        var mergeRequest = await _mergeRequestQuery.GetMergeRequestAsync(mergeRequestId);

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

        await _fixture.Mediator.Send(command);

        var mergeRequest = await _mergeRequestQuery.GetMergeRequestAsync(mergeRequestId);

        Assert.Equal(command.Title, mergeRequest.Title);
        Assert.Equal(command.Description, mergeRequest.Description);
    }

    [Fact]
    public async Task CloseMergeRequest_IsSuccessful()
    {
        var mergeRequestId = await CreateOpenMergeRequestAsync();

        await _fixture.Mediator.Send(new CloseMergeRequestCommand(mergeRequestId));

        var mergeRequest = await _mergeRequestQuery.GetMergeRequestAsync(mergeRequestId);

        Assert.Equal(MergeRequestStatus.Closed, mergeRequest.Status);
    }

    [Fact]
    public async Task ReopenMergeRequest_IsSuccessful()
    {
        var mergeRequestId = await CreateOpenMergeRequestAsync();

        await _fixture.Mediator.Send(new CloseMergeRequestCommand(mergeRequestId));
        await _fixture.Mediator.Send(new ReopenMergeRequestCommand(mergeRequestId));

        var mergeRequest = await _mergeRequestQuery.GetMergeRequestAsync(mergeRequestId);

        Assert.Equal(MergeRequestStatus.Open, mergeRequest.Status);
    }

    private async Task<Guid> CreateOpenMergeRequestAsync()
    {
        var noteId = await _fixture.CreateDefaultNoteAsync(new NoteOptions());

        var cloneNoteId = await _fixture.Mediator.Send(new CloneNoteCommand(noteId, Guid.NewGuid()));

        var command = new CreateMergeRequestCommand
        {
            NoteId = cloneNoteId,
            Title = "New Feature",
            Description = "New feature description"
        };

        return await _fixture.Mediator.Send(command);
    }
}