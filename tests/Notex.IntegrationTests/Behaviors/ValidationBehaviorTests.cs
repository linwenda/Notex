using System.Threading.Tasks;
using Notex.Core.Exceptions;
using Notex.Messages.Spaces.Commands;
using Xunit;

namespace Notex.IntegrationTests.Behaviors;

public class ValidationBehaviorTests : IClassFixture<IntegrationFixture>
{
    private readonly IntegrationFixture _fixture;

    public ValidationBehaviorTests(IntegrationFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Validate_InvalidCommand_ThrowEx()
    {
        var exception = await Assert.ThrowsAsync<InvalidCommandException>(async () =>
            await _fixture.Mediator.Send(new CreateSpaceCommand()));

        Assert.StartsWith("Invalid command, reason:", exception.Message);
    }
}