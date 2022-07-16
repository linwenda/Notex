using System;
using System.Threading.Tasks;
using MediatR;
using Notex.Messages.Shared;
using Notex.Messages.Spaces.Commands;
using Notex.Messages.Spaces.Queries;
using Xunit;

namespace Notex.IntegrationTests.Spaces;

[Collection("Sequence")]
public class SpaceTests : IClassFixture<StartupFixture>, IDisposable
{
    private readonly IMediator _mediator;
    private readonly TestHelper _creationTestHelper;

    public SpaceTests(StartupFixture fixture)
    {
        _mediator = fixture.GetService<IMediator>();
        _creationTestHelper = fixture.GetService<TestHelper>();
    }

    [Fact]
    public async Task CreateSpace_IsSuccessful()
    {
        var request = new CreateSpaceCommand
        {
            Name = "Family 2",
            Cover = "https://img.microsoft.com",
            Visibility = Visibility.Private
        };

        var spaceId = await _mediator.Send(request);
        var space = await _mediator.Send(new GetSpaceQuery(spaceId));

        Assert.Equal(request.Name, space.Name);
        Assert.Equal(request.Cover, space.Cover);
        Assert.Equal(request.Visibility, space.Visibility);
    }

    [Fact]
    public async Task UpdateSpace_IsSuccessful()
    {
        var spaceId = await _creationTestHelper.CreateDefaultSpaceAsync();

        var request = new UpdateSpaceCommand
        {
            SpaceId = spaceId,
            Name = "Family",
            Cover = "https://img.microsoft.com",
            Visibility = Visibility.Private
        };

        await _mediator.Send(request);

        var space = await _mediator.Send(new GetSpaceQuery(spaceId));

        Assert.Equal(request.Name, space.Name);
        Assert.Equal(request.Cover, space.Cover);
        Assert.Equal(request.Visibility, space.Visibility);
    }

    [Fact]
    public async Task DeleteSpace_IsSuccessful()
    {
        var spaceId = await _creationTestHelper.CreateDefaultSpaceAsync();

        await _mediator.Send(new DeleteSpaceCommand(spaceId));

        var space = await _mediator.Send(new GetSpaceQuery(spaceId));

        Assert.True(space.IsDeleted);
    }

    public void Dispose()
    {
        _creationTestHelper.CleanDatabaseAsync().GetAwaiter().GetResult();
    }
}