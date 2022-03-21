using System.Threading.Tasks;
using Notex.Core.Queries;
using Notex.Messages.Shared;
using Notex.Messages.Spaces.Commands;
using Xunit;

namespace Notex.IntegrationTests.Spaces;

[Collection(IntegrationCollection.Application)]
public class SpaceTests : IClassFixture<IntegrationFixture>
{
    private readonly IntegrationFixture _fixture;
    private readonly ISpaceQuery _spaceQuery;

    public SpaceTests(IntegrationFixture fixture)
    {
        _fixture = fixture;
        _spaceQuery = fixture.GetService<ISpaceQuery>();
    }

    [Fact]
    public async Task CreateSpace_IsSuccessful()
    {
        var request = new CreateSpaceCommand
        {
            Name = "Family 2",
            BackgroundImage = "https://img.microsoft.com",
            Visibility = Visibility.Private
        };

        var spaceId = await _fixture.Mediator.Send(request);
        var space = await _spaceQuery.GetSpaceAsync(spaceId);

        Assert.Equal(request.Name, space.Name);
        Assert.Equal(request.BackgroundImage, space.BackgroundImage);
        Assert.Equal(request.Visibility, space.Visibility);
    }

    [Fact]
    public async Task EditSpace_IsSuccessful()
    {
        var spaceId = await _fixture.GetOrCreateDefaultSpaceAsync();

        var request = new UpdateSpaceCommand
        {
            SpaceId = spaceId,
            Name = "Family",
            BackgroundImage = "https://img.microsoft.com",
            Visibility = Visibility.Private
        };

        await _fixture.Mediator.Send(request);

        var space = await _spaceQuery.GetSpaceAsync(spaceId);

        Assert.Equal(request.Name, space.Name);
        Assert.Equal(request.BackgroundImage, space.BackgroundImage);
        Assert.Equal(request.Visibility, space.Visibility);
    }

    [Fact]
    public async Task DeleteSpace_IsSuccessful()
    {
        var spaceId = await _fixture.GetOrCreateDefaultSpaceAsync();

        await _fixture.Mediator.Send(new DeleteSpaceCommand(spaceId));

        var space = await _spaceQuery.GetSpaceAsync(spaceId);

        Assert.Null(space);
    }
}