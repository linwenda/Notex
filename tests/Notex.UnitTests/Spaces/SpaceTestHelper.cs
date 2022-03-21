using System;
using Moq;
using Notex.Core.Aggregates.Spaces;
using Notex.Core.Aggregates.Spaces.DomainServices;
using Notex.Messages.Shared;

namespace Notex.UnitTests.Spaces;

public static class SpaceTestHelper
{
    public static Space CreateSpace(SpaceOptions options)
    {
        var mockSpaceChecker = new Mock<ISpaceChecker>();

        mockSpaceChecker.Setup(s => s.IsUniqueNameInUserSpace(It.IsAny<Guid>(), It.IsAny<string>()))
            .Returns(true);

        return Space.Initialize(mockSpaceChecker.Object,
            options.UserId ?? Guid.NewGuid(),
            options.Name,
            options.BackgroundImage,
            options.Visibility);
    }
}

public class SpaceOptions
{
    public Guid? UserId { get; set; }
    public string Name { get; set; } = "Space";
    public Visibility Visibility { get; set; } = Visibility.Private;
    public string BackgroundImage { get; set; } = "Background";
}