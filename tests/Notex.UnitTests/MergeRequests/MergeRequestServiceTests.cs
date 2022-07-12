using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Notex.Core.Domain.MergeRequests;
using Notex.Core.Domain.MergeRequests.Exceptions;
using Notex.Core.Domain.MergeRequests.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.MergeRequests.Events;
using Notex.UnitTests.Notes;
using Xunit;

namespace Notex.UnitTests.MergeRequests;

public class MergeRequestServiceTests
{
    private readonly IMergeRequestService _mergeRequestService;

    private readonly Mock<IReadOnlyRepository<MergeRequestDetail>> _mockRepository;

    public MergeRequestServiceTests()
    {
        _mockRepository = new Mock<IReadOnlyRepository<MergeRequestDetail>>();
        _mergeRequestService = new MergeRequestService(_mockRepository.Object);
    }

    [Fact]
    public async Task CreateMergeRequest_NotClonedNote_ThrowEx()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions {IsClone = false});

        await Assert.ThrowsAsync<OnlyClonedNoteCanCreateMergeRequestException>(async () =>
            await _mergeRequestService.CreateMergeRequestAsync(note, Guid.NewGuid(), "title", "desc"));
    }

    [Fact]
    public async Task CreateMergeRequest_HasOpenMergeRequest_ThrowEx()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions {IsClone = true});

        _mockRepository.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<MergeRequestDetail, bool>>>(),
            CancellationToken.None)).ReturnsAsync(true);

        await Assert.ThrowsAsync<MergeRequestHasBeenExistsException>(async () =>
            await _mergeRequestService.CreateMergeRequestAsync(note, Guid.NewGuid(), "title", "desc"));
    }

    [Fact]
    public async Task Create_IsSuccessful()
    {
        var note = await NoteTestHelper.CreateNote(new NoteOptions {IsClone = true});

        _mockRepository.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<MergeRequestDetail, bool>>>(),
            CancellationToken.None)).ReturnsAsync(false);

        var mergeRequest = await _mergeRequestService.CreateMergeRequestAsync(note, Guid.NewGuid(), "title", "desc");

        var mergeRequestCreatedEvent = mergeRequest.PopUncommittedEvents().Have<MergeRequestCreatedEvent>();

        Assert.Equal(note.Id, mergeRequestCreatedEvent.SourceNoteId);
        Assert.Equal(note.GetCloneFromId(), mergeRequestCreatedEvent.DestinationNoteId);
        Assert.Equal("title", mergeRequestCreatedEvent.Title);
        Assert.Equal("desc", mergeRequestCreatedEvent.Description);
    }
}