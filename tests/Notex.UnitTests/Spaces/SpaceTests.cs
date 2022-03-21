using System;
using Moq;
using Notex.Core.Aggregates.Spaces;
using Notex.Core.Aggregates.Spaces.DomainServices;
using Notex.Core.Aggregates.Spaces.Exceptions;
using Notex.Messages.Shared;
using Notex.Messages.Spaces.Events;
using Xunit;

namespace Notex.UnitTests.Spaces;

public class SpaceTests
{
    private readonly Guid _userId;
    private readonly Mock<ISpaceChecker> _mockSpaceChecker;

    public SpaceTests()
    {
        _userId = Guid.NewGuid();
        _mockSpaceChecker = new Mock<ISpaceChecker>();
    }

    [Fact]
    public void New_WithUniqueName_IsSuccessful()
    {
        _mockSpaceChecker.Setup(s => s.IsUniqueNameInUserSpace(_userId, It.IsAny<string>()))
            .Returns(true);

        var space = Space.Initialize(_mockSpaceChecker.Object, _userId, "space", "background", Visibility.Private);

        var spaceCreatedEvent = space.PopUncommittedEvents().Have<SpaceCreatedEvent>();

        Assert.Equal("space", spaceCreatedEvent.Name);
        Assert.Equal("background", spaceCreatedEvent.BackgroundImage);
        Assert.Equal(Visibility.Private, spaceCreatedEvent.Visibility);
    }

    [Fact]
    public void New_NameAlreadyExists_ThrowEx()
    {
        _mockSpaceChecker.Setup(s => s.IsUniqueNameInUserSpace(_userId, It.IsAny<string>())).Returns(false);

        Assert.Throws<SpaceNameAlreadyExistsException>(() =>
            Space.Initialize(_mockSpaceChecker.Object, _userId, "space", "background", Visibility.Private));
    }

    [Fact]
    public void Update_WhenModifiedNameExists_ThrowEx()
    {
        var space = SpaceTestHelper.CreateSpace(new SpaceOptions());

        _mockSpaceChecker.Setup(s => s.IsUniqueNameInUserSpace(_userId, It.IsAny<string>())).Returns(false);

        Assert.Throws<SpaceNameAlreadyExistsException>(() =>
            space.Update(_mockSpaceChecker.Object, "space-2", "background", Visibility.Private));
    }

    [Fact]
    public void Update_WhenNameNotChanged_NotInvokeSpaceValidatorService()
    {
        var options = new SpaceOptions();

        var space = SpaceTestHelper.CreateSpace(options);

        space.Update(_mockSpaceChecker.Object, options.Name, "background", Visibility.Private);

        _mockSpaceChecker.Verify(s => s.IsUniqueNameInUserSpace(_userId, It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void Update_IsSuccessful()
    {
        var space = SpaceTestHelper.CreateSpace(new SpaceOptions {UserId = _userId});

        _mockSpaceChecker.Setup(s => s.IsUniqueNameInUserSpace(_userId, It.IsAny<string>())).Returns(true);

        const string spaceName = "space-2";
        const string backgroundImage = "background-2";
        const Visibility visibility = Visibility.Public;

        space.Update(_mockSpaceChecker.Object, spaceName, backgroundImage, visibility);

        var spaceUpdatedEvent = space.PopUncommittedEvents().Have<SpaceUpdatedEvent>();

        Assert.Equal(spaceName, spaceUpdatedEvent.Name);
        Assert.Equal(backgroundImage, spaceUpdatedEvent.BackgroundImage);
        Assert.Equal(visibility, spaceUpdatedEvent.Visibility);
    }
}