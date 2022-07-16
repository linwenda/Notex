using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Domain.Spaces;
using Notex.Core.Domain.Spaces.ReadModels;
using Notex.Messages.Shared;

namespace Notex.UnitTests.Spaces;

public static class SpaceTestHelper
{
    public static async Task<Space> CreateSpace(SpaceOptions options)
    {
        var mockReadOnlyRepository = new Mock<IReadOnlyRepository<SpaceDetail>>();

        mockReadOnlyRepository
            .Setup(r => r.CountAsync(It.IsAny<Expression<Func<SpaceDetail, bool>>>(), CancellationToken.None))
            .ReturnsAsync(0);

        var mockSpaceService = new SpaceService(mockReadOnlyRepository.Object);

        return await mockSpaceService.CreateSpaceAsync(
            options.UserId ?? Guid.NewGuid(),
            options.Name,
            options.Cover,
            options.Visibility);
    }
}

public class SpaceOptions
{
    public Guid? UserId { get; set; }
    public string Name { get; set; } = "__space__";
    public Visibility Visibility { get; set; } = Visibility.Private;
    public string Cover { get; set; } = "__cover__";
}