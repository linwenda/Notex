using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Notex.IntegrationTests.Notes;
using Notex.Messages.MergeRequests;
using Notex.Messages.MergeRequests.Commands;
using Notex.Messages.MergeRequests.Queries;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Commands;
using Notex.Messages.Notes.Queries;
using TestStack.BDDfy;
using Xunit;

namespace Notex.IntegrationTests.BDD;

[Collection("Sequence")]
public class MergeNoteStory : IClassFixture<StartupFixture>
{
    private readonly IMediator _mediator;
    private readonly TestHelper _creationTestHelper;

    private Guid _noteId;
    private Guid _cloneNoteId;
    private Guid _mergeRequestId;
    private EditNoteCommand _editNoteCommand;

    public MergeNoteStory(StartupFixture fixture)
    {
        _mediator = fixture.GetService<IMediator>();
        _creationTestHelper = fixture.GetService<TestHelper>();
    }
    
    private async Task GivenCloneNote()
    {
        _noteId = await _creationTestHelper.CreateDefaultNoteAsync(new NoteOptions { Status = NoteStatus.Published });
        
        _cloneNoteId = await _mediator.Send(new CloneNoteCommand(_noteId, Guid.NewGuid()));

        _editNoteCommand = new EditNoteCommand(_cloneNoteId, "edited title", "edited content", "comment");

        await _mediator.Send(_editNoteCommand);
    }

    private async Task WhenMergeRequestIsMerged()
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
        var note = await _mediator.Send(new GetNoteQuery(_noteId));

        Assert.Equal(2, note.Version);
        Assert.Equal(_editNoteCommand.Title, note.Title);
        Assert.Equal(_editNoteCommand.Content, note.Content);

        var noteHistories = await _mediator.Send(new GetNoteHistoriesQuery(note.Id));
        Assert.Equal(2, noteHistories.Count());
    }

    private async Task AndTheCloneNoteShouldBeDeleted()
    {
        var cloneNote = await _mediator.Send(new GetNoteQuery(_cloneNoteId));

        Assert.True(cloneNote.IsDeleted);
    }

    private async Task AndTheMergeRequestStatusIsMerged()
    {
        var mergeRequest = await _mediator.Send(new GetMergeRequestQuery(_mergeRequestId));

        Assert.Equal(MergeRequestStatus.Merged, mergeRequest.Status);
    }

    [Fact]
    public void Execute()
    {
        this.BDDfy();
    }
}