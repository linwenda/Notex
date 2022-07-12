using System;
using Notex.Core.Domain.MergeRequests;
using Notex.Core.Domain.MergeRequests.Exceptions;
using Notex.Messages.MergeRequests.Events;
using Xunit;

namespace Notex.UnitTests.MergeRequests;

public class MergeRequestTests
{
    private readonly Guid _userId;

    public MergeRequestTests()
    {
        _userId = Guid.NewGuid();
    }

    
    [Fact]
    public void Initialize_IsSuccessful()
    {
        var initializeInput = new
        {
            UserId = Guid.NewGuid(),
            SourceNoteId = Guid.NewGuid(),
            DestinationNoteId = Guid.NewGuid(),
            Title = "test",
            Description = "desc"
        };

        var mergeRequest = MergeRequest.Initialize(
            initializeInput.UserId,
            initializeInput.SourceNoteId,
            initializeInput.DestinationNoteId,
            initializeInput.Title,
            initializeInput.Description);

        var mergeRequestCreatedEvent = mergeRequest.PopUncommittedEvents().Have<MergeRequestCreatedEvent>();

        Assert.Equal(initializeInput.UserId, mergeRequestCreatedEvent.CreatorId);
        Assert.Equal(initializeInput.Title, mergeRequestCreatedEvent.Title);
        Assert.Equal(initializeInput.Description, mergeRequestCreatedEvent.Description);
        Assert.Equal(initializeInput.SourceNoteId, mergeRequestCreatedEvent.SourceNoteId);
        Assert.Equal(initializeInput.DestinationNoteId, mergeRequestCreatedEvent.DestinationNoteId);
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

        mergeRequest.SetMergeStatus(_userId);

        mergeRequest.PopUncommittedEvents().Have<MergeRequestMergedEvent>();
    }

    [Fact]
    public void Merge_WhenMergeRequestClosed_ThrowEx()
    {
        var mergeRequest = CreateOpenMergeRequest();

        mergeRequest.Close(_userId);

        Assert.Throws<MergeRequestStatusChangeException>(() =>
            mergeRequest.SetMergeStatus(_userId));
    }

    [Fact]
    public void Reopen_WhenMergeRequestMerged_ThrowEx()
    {
        var mergeRequest = CreateOpenMergeRequest();

        mergeRequest.SetMergeStatus(_userId);

        Assert.Throws<MergeRequestStatusChangeException>(() => mergeRequest.ReOpen(_userId));
    }

    [Fact]
    public void Reopen_WhenMergeRequestClosed_IsSuccessful()
    {
        var mergeRequest = CreateOpenMergeRequest();

        mergeRequest.Close(_userId);

        mergeRequest.ReOpen(_userId);

        mergeRequest.PopUncommittedEvents().Have<MergeRequestReopenedEvent>();
    }

    private static MergeRequest CreateOpenMergeRequest()
    {
        return MergeRequest.Initialize(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "title", "description");
    }
}