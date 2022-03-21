using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Notex.Core.Queries;
using Notex.IntegrationTests.Notes;
using Notex.Messages.MergeRequests;
using Notex.Messages.MergeRequests.Commands;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Commands;
using TestStack.BDDfy;
using Xunit;

namespace Notex.IntegrationTests.BDD;

[Collection(IntegrationCollection.Application)]
public class MergeNoteStory : IClassFixture<IntegrationFixture>
{
    private readonly Guid _noteId;
    private readonly IMediator _mediator;
    private readonly INoteQuery _noteQuery;
    private readonly IMergeRequestQuery _mergeRequestQuery;

    private Guid _cloneNoteId;
    private Guid _mergeRequestId;
    private EditNoteCommand _editNoteCommand;

    public MergeNoteStory(IntegrationFixture fixture)
    {
        _noteId = fixture.CreateDefaultNoteAsync(new NoteOptions { Status = NoteStatus.Published }).GetAwaiter()
            .GetResult();
        
        _mediator = fixture.GetService<IMediator>();
        _noteQuery = fixture.GetService<INoteQuery>();
        _mergeRequestQuery = fixture.GetService<IMergeRequestQuery>();
    }

    private async Task GivenCloneNote()
    {
        _cloneNoteId = await _mediator.Send(new CloneNoteCommand(_noteId, Guid.NewGuid()));

        _editNoteCommand = new EditNoteCommand(_cloneNoteId, "edited title", "edited content", "comment");

        await _mediator.Send(_editNoteCommand);
    }

    private async Task WhenCreateMergeRequestIsMerged()
    {
        var createMergeRequestCommand = new CreateMergeRequestCommand
        {
            NoteId = _cloneNoteId,
            Title = "merge request title",
            Description = "merge request description"
        };

        _mergeRequestId = await _mediator.Send(createMergeRequestCommand);

        await _mediator.Send(new MergeTheRequestCommand(_mergeRequestId));
    }

    private async Task ThenTheSourceNoteShouldBeUpdated()
    {
        var note = await _noteQuery.GetNoteAsync(_noteId);

        Assert.Equal(2, note.Version);
        Assert.Equal(_editNoteCommand.Title, note.Title);
        Assert.Equal(_editNoteCommand.Content, note.Content);

        var noteHistories = await _noteQuery.GetHistoriesAsync(note.Id);
        Assert.Equal(2, noteHistories.Count());
    }

    private async Task AndTheCloneNoteShouldBeDeleted()
    {
        var cloneNote = await _noteQuery.GetNoteAsync(_cloneNoteId);

        Assert.Null(cloneNote);
    }

    private async Task AndTheMergeRequestStatusIsMerged()
    {
        var mergeRequest = await _mergeRequestQuery.GetMergeRequestAsync(_mergeRequestId);

        Assert.Equal(MergeRequestStatus.Merged, mergeRequest.Status);
    }

    [Fact]
    public void Execute()
    {
        this.BDDfy();
    }
}