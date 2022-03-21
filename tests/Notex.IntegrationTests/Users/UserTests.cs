using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Aggregates;
using Notex.Core.Aggregates.Users.ReadModels;
using Notex.Core.Queries;
using Notex.Messages.Users.Commands;
using Xunit;

namespace Notex.IntegrationTests.Users;

[Collection(IntegrationCollection.Application)]
public class UserTests : IClassFixture<IntegrationFixture>
{
    private readonly IMediator _mediator;
    private readonly IUserQuery _userQuery;
    private readonly IReadModelRepository _readModelRepository;

    public UserTests(IntegrationFixture fixture)
    {
        _mediator = fixture.GetService<IMediator>();
        _userQuery = fixture.GetService<IUserQuery>();
        _readModelRepository = fixture.GetService<IReadModelRepository>();
    }

    [Fact]
    public async Task RegisterUser_IsSuccessful()
    {
        var command = new RegisterUserCommand
        {
            Email = "bruce@outlook.com",
            Name = "bruce.lin",
            Password = "123456"
        };

        var userId = await _mediator.Send(command);
        var user = await _userQuery.GetUserAsync(userId);

        Assert.Equal(command.Email, user.Email);
        Assert.Equal(command.Email, user.UserName);
        Assert.Equal(command.Name, user.Name);
        Assert.True(user.IsActive);
    }

    [Theory]
    [InlineData("test@outlook.com", "123456", false)]
    [InlineData("peter@outlook.com", "654321", false)]
    [InlineData("peter@outlook.com", "123456", true)]
    public async Task Authenticate_GivenCredentials_ReturnsExpectedResult(string email, string password, bool expect)
    {
        var user = await CreateDefaultUser();

        var result = await _mediator.Send(new AuthenticateCommand(email, password));

        if (expect)
        {
            Assert.True(result.IsAuthenticated);
            Assert.Equal(user.Email, result.User.Email);
            Assert.Equal(user.Name, result.User.Name);
        }
        else
        {
            Assert.False(result.IsAuthenticated);
            Assert.Equal("Incorrect login or password", result.AuthenticationError);
        }
    }

    private async Task<UserDetail> CreateDefaultUser()
    {
        var command = new RegisterUserCommand
        {
            Email = "peter@outlook.com",
            Name = "peter.t",
            Password = "123456"
        };

        var user = await _readModelRepository.Query<UserDetail>().FirstOrDefaultAsync(u => u.Email == command.Email);

        if (user == null)
        {
            var userId = await _mediator.Send(command);

            user = await _readModelRepository.Query<UserDetail>().FirstOrDefaultAsync(u => u.Id == userId);
        }

        return user;
    }
}