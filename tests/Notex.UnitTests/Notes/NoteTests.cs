using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notex.Core.Domain.Notes.Exceptions;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Events;
using Notex.Messages.Shared;
using Notex.UnitTests.Spaces;
using Xunit;

namespace Notex.UnitTests.Notes;

public class NoteTests
{
    private readonly Guid _userId;

    public NoteTests()
    {
        _userId = Guid.NewGuid();
    }

    [Fact]
    public async Task Create_IsSuccessful()
    {
        var space = await SpaceTestHelper.CreateSpace(new SpaceOptions());

        var note = space.CreateNote(".NET 6", ".NET 6 new feature", NoteStatus.Published);

        var noteCreatedEvent = note.PopUncommittedEvents().Have<NoteCreatedEvent>();

        Assert.Equal(".NET 6", noteCreatedEvent.Title);
        Assert.Equal(".NET 6 new feature", noteCreatedEvent.Content);
        Assert.Equal(NoteStatus.Published, noteCreatedEvent.Status);
        Assert.Equal(Visibility.Public, noteCreatedEvent.Visibility);
    }

    [Fact]
    public async Task Edit_IsSuccessful()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions());

        note.Edit(_userId, "Java", "Java content", "comment");

        var noteEditedEvent = note.PopUncommittedEvents().Have<NoteEditedEvent>();

        Assert.Equal("Java", noteEditedEvent.Title);
        Assert.Equal("Java content", noteEditedEvent.Content);
    }

    [Fact]
    public async Task Delete_IsSuccessful()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions());

        note.Delete();
        note.PopUncommittedEvents().Have<NoteDeletedEvent>();
    }

    [Fact]
    public async Task Delete_HasBeenDeleted_OnlyApplyChangeOnce()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions());

        note.Delete();
        note.Delete();
        Assert.NotNull(note.PopUncommittedEvents().SingleOrDefault(e => e.GetType() == typeof(NoteDeletedEvent)));
    }

    [Fact]
    public async Task Publish_IsSuccessful()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions {Status = NoteStatus.Draft});

        note.Publish();
        note.PopUncommittedEvents().Have<NotePublishedEvent>();
    }

    [Fact]
    public async Task Publish_HasBeenDeleted_ThrowEx()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions {Status = NoteStatus.Draft});

        note.Delete();

        Assert.Throws<NoteHasBeenDeletedException>(() => note.Publish());
    }

    [Fact]
    public async Task Publish_HasBeenPublished_OnlyApplyChangeOnce()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions {Status = NoteStatus.Draft});

        note.Publish();
        note.Publish();

        Assert.NotNull(note.PopUncommittedEvents().SingleOrDefault(e => e.GetType() == typeof(NotePublishedEvent)));
    }

    [Fact]
    public async Task ChangeVisibility_IsSuccessful()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions {Status = NoteStatus.Published});

        note.ChangeVisibility(Visibility.Private);

        var noteVisibilityChangedEvent = note.PopUncommittedEvents().Have<NoteVisibilityChangedEvent>();
        Assert.Equal(Visibility.Private, noteVisibilityChangedEvent.Visibility);
    }

    [Fact]
    public async Task ChangeVisibility_DraftNote_NotApplyChange()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions());

        note.ChangeVisibility(Visibility.Public);

        note.PopUncommittedEvents().NotHave<NoteVisibilityChangedEvent>();
    }

    [Fact]
    public async Task UpdateTags_IsSuccessful()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions());

        var tags = new List<string> {"cqrs", "ddd"};

        note.UpdateTags(tags);

        var noteTagsUpdatedEvent = note.PopUncommittedEvents().Have<NoteTagsUpdatedEvent>();

        Assert.True(noteTagsUpdatedEvent.Tags.SequenceEqual(tags));
    }

    [Fact]
    public async Task Clone_DraftNote_ThrowEx()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions {Status = NoteStatus.Draft});

        Assert.Throws<NoteHaveNotBeenPublishedException>(() => note.Clone(_userId, Guid.NewGuid()));
    }

    [Fact]
    public async Task Clone_IsSuccessful()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions());

        var cloneNote = note.Clone(_userId, Guid.NewGuid());

        var noteCreatedEvent = cloneNote.PopUncommittedEvents().Have<NoteCreatedEvent>();

        Assert.Equal(note.Id, noteCreatedEvent.CloneFromId);
        Assert.Equal(cloneNote.Id, noteCreatedEvent.SourcedId);
        Assert.Equal(_userId, noteCreatedEvent.CreatorId);
        Assert.Equal(NoteStatus.Published, noteCreatedEvent.Status);
    }
}