using System;
using Moq;
using Notex.Core.Aggregates.MergeRequests;
using Notex.Core.Aggregates.MergeRequests.DomainServices;
using Notex.Core.Aggregates.MergeRequests.Exceptions;
using Notex.Core.Aggregates.Notes;
using Notex.Core.Aggregates.Notes.DomainServices;
using Notex.Core.Aggregates.Notes.Exceptions;
using Notex.Messages.MergeRequests.Events;
using Notex.Messages.Notes.Events;
using Notex.UnitTests.Notes;
using Xunit;

namespace Notex.UnitTests.MergeRequests;

public class MergeRequestTests
{
    private readonly Mock<INoteChecker> _mockNoteChecker;
    private readonly Mock<IMergeRequestChecker> _mockMergeRequestChecker;
    private readonly Guid _userId;

    public MergeRequestTests()
    {
        _mockNoteChecker = new Mock<INoteChecker>();
        _mockMergeRequestChecker = new Mock<IMergeRequestChecker>();
        _userId = Guid.NewGuid();
    }

    [Fact]
    public void Create_NotCloneNote_ThrowEx()
    {
        var note = NoteTestHelper.CreateNote(new NoteOptions());

        Assert.Throws<OnlyCloneNoteCanBeMergedException>(() =>
            note.CreateMergeRequest(_mockNoteChecker.Object, _mockMergeRequestChecker.Object, _userId, "title",
                "description"));
    }

    [Fact]
    public void Create_InvalidDestinationNote_ThrowEx()
    {
        var cloneNote = CreateCloneNote();

        _mockNoteChecker.Setup(n => n.IsPublishedNote(Guid.NewGuid())).Returns(false);

        Assert.Throws<NoteHaveNotBeenPublishedException>(() => cloneNote.CreateMergeRequest(
            _mockNoteChecker.Object, _mockMergeRequestChecker.Object, _userId, "title", "description"));
    }

    [Fact]
    public void Create_ExistOpenMergeRequest_ThrowEx()
    {
        var cloneNote = CreateCloneNote();

        _mockNoteChecker.Setup(n => n.IsPublishedNote(It.IsAny<Guid>())).Returns(true);

        _mockMergeRequestChecker.Setup(n => n.HasOpenMergeRequest(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(true);

        Assert.Throws<MergeRequestHasBeenExistsException>(() => cloneNote.CreateMergeRequest(
            _mockNoteChecker.Object, _mockMergeRequestChecker.Object, _userId, "title", "description"));
    }

    [Fact]
    public void Create_IsSuccessful()
    {
        var cloneNote = CreateCloneNote();
        var noteCreatedEvent = cloneNote.PopUncommittedEvents().Have<NoteCreatedEvent>();

        _mockNoteChecker.Setup(n => n.IsPublishedNote(It.IsAny<Guid>())).Returns(true);

        _mockMergeRequestChecker.Setup(n => n.HasOpenMergeRequest(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(false);

        var mergeRequest = cloneNote.CreateMergeRequest(_mockNoteChecker.Object, _mockMergeRequestChecker.Object,
            _userId, "title", "description");

        var mergeRequestCreatedEvent = mergeRequest.PopUncommittedEvents().Have<MergeRequestCreatedEvent>();

        Assert.Equal("title", mergeRequestCreatedEvent.Title);
        Assert.Equal("description", mergeRequestCreatedEvent.Description);
        Assert.Equal(cloneNote.Id, mergeRequestCreatedEvent.SourceNoteId);
        Assert.Equal(noteCreatedEvent.CloneFormId, mergeRequestCreatedEvent.DestinationNoteId);
    }

    [Fact]
    public void Update_IsSuccessful()
    {
        var mergeRequest = CreateOpenMergeRequest();

        mergeRequest.Update(_userId, "title 2", "description 2");

        var mergeRequestUpdatedEvent = mergeRequest.PopUncommittedEvents().Have<MergeRequestUpdatedEvent>();

        Assert.Equal("title 2", mergeRequestUpdatedEvent.Title);
        Assert.Equal("description 2", mergeRequestUpdatedEvent.Description);
    }

    [Fact]
    public void Update_WhenMergeRequestClosed_ThrowEx()
    {
        var mergeRequest = CreateOpenMergeRequest();

        mergeRequest.Close(_userId);

        Assert.Throws<OnlyOpenMergeRequestCanBeUpdatedException>(() =>
            mergeRequest.Update(_userId, "title 2", "description 2"));
    }

    [Fact]
    public void Close_IsSuccessful()
    {
        var mergeRequest = CreateOpenMergeRequest();

        mergeRequest.Close(_userId);

        var mergeRequestClosedEvent = mergeRequest.PopUncommittedEvents().Have<MergeRequestClosedEvent>();

        Assert.Equal(_userId, mergeRequestClosedEvent.ReviewerId);
    }

    [Fact]
    public void Close_WhenMergeRequestClosed_ThrowEx()
    {
        var mergeRequest = CreateOpenMergeRequest();

        mergeRequest.Close(_userId);

        Assert.Throws<MergeRequestStatusChangeException>(() => mergeRequest.Close(_userId));
    }

    [Fact]
    public void Merge_IsSuccessful()
    {
        var mergeRequest = CreateOpenMergeRequest();

        _mockNoteChecker.Setup(n => n.IsPublishedNote(It.IsAny<Guid>())).Returns(true);

        mergeRequest.Merge(_mockNoteChecker.Object, _userId);

        mergeRequest.PopUncommittedEvents().Have<MergeRequestMergedEvent>();
    }

    [Fact]
    public void Merge_InvalidNote_ThrowEx()
    {
        var mergeRequest = CreateOpenMergeRequest();

        _mockNoteChecker.Setup(n => n.IsPublishedNote(It.IsAny<Guid>())).Returns(false);

        Assert.Throws<NoteHaveNotBeenPublishedException>(() =>
            mergeRequest.Merge(_mockNoteChecker.Object, _userId));
    }

    [Fact]
    public void Merge_WhenMergeRequestClosed_ThrowEx()
    {
        var mergeRequest = CreateOpenMergeRequest();

        mergeRequest.Close(_userId);

        Assert.Throws<MergeRequestStatusChangeException>(() =>
            mergeRequest.Merge(_mockNoteChecker.Object, _userId));
    }

    [Fact]
    public void Reopen_WhenMergeRequestMerged_ThrowEx()
    {
        var mergeRequest = CreateOpenMergeRequest();

        _mockNoteChecker.Setup(n => n.IsPublishedNote(It.IsAny<Guid>())).Returns(true);

        mergeRequest.Merge(_mockNoteChecker.Object, _userId);

        Assert.Throws<MergeRequestStatusChangeException>(() => mergeRequest.Reopen(_mockNoteChecker.Object, _userId));
    }

    [Fact]
    public void Reopen_WhenMergeRequestClosed_IsSuccessful()
    {
        var mergeRequest = CreateOpenMergeRequest();

        mergeRequest.Close(_userId);

        _mockNoteChecker.Setup(n => n.IsPublishedNote(It.IsAny<Guid>())).Returns(true);

        mergeRequest.Reopen(_mockNoteChecker.Object, _userId);

        mergeRequest.PopUncommittedEvents().Have<MergeRequestReopenedEvent>();
    }

    private Note CreateCloneNote()
    {
        var note = NoteTestHelper.CreateNote(new NoteOptions());

        return note.Clone(_userId, Guid.NewGuid());
    }

    private MergeRequest CreateOpenMergeRequest()
    {
        var cloneNote = CreateCloneNote();

        _mockNoteChecker.Setup(n => n.IsPublishedNote(It.IsAny<Guid>())).Returns(true);

        _mockMergeRequestChecker.Setup(n => n.HasOpenMergeRequest(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(false);

        return cloneNote.CreateMergeRequest(_mockNoteChecker.Object, _mockMergeRequestChecker.Object, _userId, "title",
            "description");
    }
}