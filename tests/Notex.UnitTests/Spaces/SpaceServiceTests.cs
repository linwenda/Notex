using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Domain.Spaces;
using Notex.Core.Domain.Spaces.Exceptions;
using Notex.Core.Domain.Spaces.ReadModels;
using Notex.Messages.Shared;
using Notex.Messages.Spaces.Events;
using Xunit;

namespace Notex.UnitTests.Spaces;

public class SpaceServiceTests
{
    private readonly Mock<IReadOnlyRepository<SpaceDetail>> _spaceDetailRepositoryMock;
    private readonly ISpaceService _spaceService;
    private readonly Guid _userId;

    public SpaceServiceTests()
    {
        _spaceDetailRepositoryMock = new Mock<IReadOnlyRepository<SpaceDetail>>();
        _spaceService = new SpaceService(_spaceDetailRepositoryMock.Object);
        _userId = Guid.NewGuid();
    }

    [Fact]
    public async Task Create_WithUniqueName_IsSuccessful()
    {
        var space = await _spaceService.CreateSpaceAsync(_userId, "space", "background", Visibility.Private);

        var spaceCreatedEvent = space.PopUncommittedEvents().Have<SpaceCreatedEvent>();

        Assert.Equal("space", spaceCreatedEvent.Name);
        Assert.Equal(_userId, spaceCreatedEvent.UserId);
        Assert.Equal("background", spaceCreatedEvent.BackgroundImage);
        Assert.Equal(Visibility.Private, spaceCreatedEvent.Visibility);
    }

    [Fact]
    public async Task Create_NameAlreadyExists_ThrowEx()
    {
        _spaceDetailRepositoryMock
            .Setup(r => r.CountAsync(It.IsAny<Expression<Func<SpaceDetail, bool>>>(), CancellationToken.None))
            .ReturnsAsync(1);

        await Assert.ThrowsAsync<SpaceNameAlreadyExistsException>(async () =>
            await _spaceService.CreateSpaceAsync(_userId, "space", "background", Visibility.Private));
    }

    [Fact]
    public async Task Update_WhenModifiedNameExists_ThrowEx()
    {
        var space = await SpaceTestHelper.CreateSpace(new SpaceOptions());

        _spaceDetailRepositoryMock
            .Setup(r => r.CountAsync(It.IsAny<Expression<Func<SpaceDetail, bool>>>(), CancellationToken.None))
            .ReturnsAsync(1);

        await Assert.ThrowsAsync<SpaceNameAlreadyExistsException>(async () =>
            await _spaceService.UpdateSpaceAsync(space, "space-2", "background", Visibility.Private));
    }
  
    
    [Fact]
    public async Task Update_IsSuccessful()
    {
        var space = await SpaceTestHelper.CreateSpace(new SpaceOptions {UserId = _userId});

        _spaceDetailRepositoryMock
            .Setup(r => r.CountAsync(It.IsAny<Expression<Func<SpaceDetail, bool>>>(), CancellationToken.None))
            .ReturnsAsync(0);

        const string spaceName = "space-2";
        const string backgroundImage = "background-2";
        const Visibility visibility = Visibility.Public;

        await _spaceService.UpdateSpaceAsync(space, spaceName, backgroundImage, visibility);

        var spaceUpdatedEvent = space.PopUncommittedEvents().Have<SpaceUpdatedEvent>();

        Assert.Equal(spaceName, spaceUpdatedEvent.Name);
        Assert.Equal(backgroundImage, spaceUpdatedEvent.BackgroundImage);
        Assert.Equal(visibility, spaceUpdatedEvent.Visibility);
    }
}