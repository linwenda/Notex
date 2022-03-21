using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Notex.Core.Queries;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Commands;
using Notex.Messages.Shared;
using Xunit;

namespace Notex.IntegrationTests.Notes;

[Collection(IntegrationCollection.Application)]
public class NoteTests : IClassFixture<IntegrationFixture>, IDisposable
{
    private readonly IntegrationFixture _fixture;
    private readonly IMediator _mediator;
    private readonly INoteQuery _noteQuery;

    public NoteTests(IntegrationFixture fixture)
    {
        _fixture = fixture;
        _mediator = fixture.GetService<IMediator>();
        _noteQuery = fixture.GetService<INoteQuery>();
    }

    [Theory]
    [InlineData(NoteStatus.Draft)]
    [InlineData(NoteStatus.Published)]
    public async Task Create_IsSuccessful(NoteStatus status)
    {
        var spaceId = await _fixture.GetOrCreateDefaultSpaceAsync();

        var command = new CreateNoteCommand
        {
            SpaceId = spaceId,
            Title = ".Net 6",
            Content = ".Net 6 new feature",
            Status = status
        };

        var noteId = await _mediator.Send(command);

        var note = await _noteQuery.GetNoteAsync(noteId);

        Assert.Equal(command.SpaceId, note.SpaceId);
        Assert.Equal(command.Title, note.Title);
        Assert.Equal(command.Content, note.Content);
        Assert.Equal(command.Status, note.Status);

        if (status == NoteStatus.Draft)
        {
            Assert.Equal(0, note.Version);
            Assert.Equal(Visibility.Private, note.Visibility);
        }
        else
        {
            Assert.Equal(1, note.Version);
            Assert.Equal(Visibility.Public, note.Visibility);

            var noteHistories = await _noteQuery.GetHistoriesAsync(noteId);
            Assert.Single(noteHistories);
        }
    }

    [Theory]
    [InlineData(NoteStatus.Draft)]
    [InlineData(NoteStatus.Published)]
    public async Task Edit_IsSuccessful(NoteStatus status)
    {
        var noteId = await _fixture.CreateDefaultNoteAsync(new NoteOptions { Status = status });

        var command = new EditNoteCommand(noteId, ".Net 7", ".Net 7 new feature #2", "");

        await _mediator.Send(command);

        var editedNote = await _noteQuery.GetNoteAsync(noteId);

        Assert.Equal(command.Title, editedNote.Title);
        Assert.Equal(command.Content, editedNote.Content);

        if (status == NoteStatus.Draft)
        {
            Assert.Equal(0, editedNote.Version);
        }
        else
        {
            Assert.Equal(2, editedNote.Version);
            
            var noteHistories = await _noteQuery.GetHistoriesAsync(noteId);
            
            Assert.Equal(2, noteHistories.Count());
        }
    }

    [Fact]
    public async Task Publish_WithDraft_IsSuccessful()
    {
        var noteId = await _fixture.CreateDefaultNoteAsync(new NoteOptions { Status = NoteStatus.Draft });

        await _mediator.Send(new PublishNoteCommand(noteId));

        var publishedNote = await _noteQuery.GetNoteAsync(noteId);

        Assert.Equal(NoteStatus.Published, publishedNote.Status);
        Assert.Equal(1, publishedNote.Version);
    }

    [Fact]
    public async Task UpdateTags_IsSuccessful()
    {
        var noteId = await _fixture.CreateDefaultNoteAsync(new NoteOptions());

        var command = new UpdateNoteTagsCommand(noteId, new[] { ".Net", "C#" });

        await _mediator.Send(command);

        var note = await _noteQuery.GetNoteAsync(noteId);

        var inFirstOnly = command.Tags.Except(note.Tags);
        var inSecondOnly = note.Tags.Except(command.Tags);

        Assert.Empty(inFirstOnly);
        Assert.Empty(inSecondOnly);
    }

    [Fact]
    public async Task Clone_IsSuccessful()
    {
        var noteId = await _fixture.CreateDefaultNoteAsync(new NoteOptions());
        var note = await _noteQuery.GetNoteAsync(noteId);

        var cloneNoteId = await _mediator.Send(new CloneNoteCommand(noteId, Guid.NewGuid()));
        var cloneNote =  await _noteQuery.GetNoteAsync(cloneNoteId);

        Assert.Equal(note.Id, cloneNote.CloneFormId);
        Assert.Equal(note.Title, cloneNote.Title);
        Assert.Equal(note.Content, cloneNote.Content);
        Assert.Equal(note.Status, cloneNote.Status);
        Assert.Equal(1, cloneNote.Version);
    }

    public void Dispose()
    {
    }
}