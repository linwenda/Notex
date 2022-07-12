using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Notex.IntegrationTests.Notes;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Commands;
using Notex.Messages.Notes.Queries;
using TestStack.BDDfy;
using Xunit;

namespace Notex.IntegrationTests.BDD;

[Collection("Sequence")]
public class RestoreNoteStory : IClassFixture<StartupFixture>
{
    private readonly IMediator _mediator;
    private readonly TestHelper _creationTestHelper;

    private Guid _noteId;
    private IEnumerable<NoteHistoryDto> _noteHistories;
    private EditNoteCommand _editNoteCommandForRestore;

    public RestoreNoteStory(StartupFixture fixture)
    {
        _mediator = fixture.GetService<IMediator>();
        _creationTestHelper = fixture.GetService<TestHelper>();
    }

    private async Task GivenEdited2TimesNote()
    {
        _noteId = await _creationTestHelper.CreateDefaultNoteAsync(new NoteOptions { Status = NoteStatus.Published });

        _editNoteCommandForRestore = new EditNoteCommand(_noteId, ".Net 7", ".Net 7 new feature", "");

        //v2
        await _mediator.Send(_editNoteCommandForRestore);

        //v3
        await _mediator.Send(new EditNoteCommand(_noteId, ".Net 8", ".Net 8 new feature", ""));

        _noteHistories = await _mediator.Send(new GetNoteHistoriesQuery(_noteId));
    }

    private async Task WhenTheNoteRestoreToV2Version()
    {
        await _mediator.Send(new RestoreNoteCommand(_noteId, _noteHistories.First(h => h.Version == 2).Id));

        _noteHistories = await _mediator.Send(new GetNoteHistoriesQuery(_noteId));
    }

    private async Task ThenTheNoteShouldBeRestored()
    {
        var note = await _mediator.Send(new GetNoteQuery(_noteId));

        Assert.Equal(4, note.Version);
        Assert.Equal(_editNoteCommandForRestore.Title, note.Title);
        Assert.Equal(_editNoteCommandForRestore.Content, note.Content);
    }

    private async Task AndTheNoteHistoryShouldBeUpdated()
    {
        _noteHistories = await _mediator.Send(new GetNoteHistoriesQuery(_noteId));
        Assert.Equal(4, _noteHistories.Count());
        Assert.Equal("Restored from v2", _noteHistories.OrderByDescending(h => h.Version).First().Comment);
    }

    [Fact]
    public void Execute()
    {
        this.BDDfy();
    }
}