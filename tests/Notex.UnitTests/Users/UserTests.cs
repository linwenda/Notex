using System.Collections.ObjectModel;
using System.Linq;
using Moq;
using Notex.Core.Aggregates.Users;
using Notex.Core.Aggregates.Users.DomainServices;
using Notex.Core.Aggregates.Users.Exceptions;
using Notex.Core.Authorization;
using Notex.Messages.Users.Events;
using Xunit;

namespace Notex.UnitTests.Users;

public class UserTests
{
    private readonly Mock<IPasswordService> _mockPasswordService;
    private readonly Mock<IUserChecker> _mockUserChecker;

    public UserTests()
    {
        _mockPasswordService = new Mock<IPasswordService>();
        _mockUserChecker = new Mock<IUserChecker>();
    }

    [Fact]
    public void Initialize_WithUniqueEmail_IsSuccessful()
    {
        _mockUserChecker.Setup(u => u.IsUniqueEmail(It.IsAny<string>())).Returns(true);

        var certificate = new
        {
            Email = "bruce@outlook.com",
            Password = "123456",
            Name = "Bruce"
        };

        var user = User.Initialize(_mockUserChecker.Object, _mockPasswordService.Object, certificate.Email,
            certificate.Password, certificate.Name);

        var userCreatedEvent = user.PopUncommittedEvents().Have<UserCreatedEvent>();

        Assert.Equal(certificate.Email, userCreatedEvent.Email);
        Assert.Equal(certificate.Name, userCreatedEvent.Name);
    }

    [Fact]
    public void Initialize_EmailAlreadyExist_ThrowEx()
    {
        _mockUserChecker.Setup(u => u.IsUniqueEmail(It.IsAny<string>())).Returns(false);

        Assert.Throws<EmailAlreadyExistsException>(() => User.Initialize(_mockUserChecker.Object,
            _mockPasswordService.Object, "bruce@outlook.com", "123456", "Bruce"));
    }

    [Fact]
    public void ChangePassword_IncorrectOldPassword_ThrowEx()
    {
        _mockPasswordService.Setup(p => p.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

        var user = CreateUser();

        Assert.Throws<PasswordIncorrectException>(() =>
            user.ChangePassword(_mockPasswordService.Object, "987654", "789456"));
    }

    [Fact]
    public void ChangePassword_IsSuccessful()
    {
        const string newPassword = "123456";

        _mockPasswordService.Setup(p => p.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        _mockPasswordService.Setup(p => p.HashPassword(It.IsAny<string>())).Returns(newPassword);

        var user = CreateUser();

        user.ChangePassword(_mockPasswordService.Object, "123456", newPassword);

        var userPasswordChangedEvent = user.PopUncommittedEvents().Have<UserPasswordChangedEvent>();

        Assert.Equal(newPassword, userPasswordChangedEvent.Password);
    }

    [Fact]
    public void UpdateProfile_IsSuccessful()
    {
        var profile = new
        {
            Name = "Bruce",
            Avatar = "https://avatar.microsoft.com",
            Bio = "Hello World"
        };

        var user = CreateUser();

        user.UpdateProfile(profile.Name, profile.Avatar, profile.Bio);

        var userProfileUpdatedEvent = user.PopUncommittedEvents().Have<UserProfileUpdatedEvent>();

        Assert.Equal(profile.Name, userProfileUpdatedEvent.Name);
        Assert.Equal(profile.Avatar, userProfileUpdatedEvent.Avatar);
        Assert.Equal(profile.Bio, userProfileUpdatedEvent.Bio);
    }

    [Fact]
    public void UpdateRoles_IsSuccessful()
    {
        var user = CreateUser();

        var roles = new Collection<string>
        {
            AuthorizationConstants.Roles.Administrator,
            AuthorizationConstants.Roles.PowerUser
        };

        user.UpdateRoles(roles);

        var userRolesUpdatedEvent = user.PopUncommittedEvents().Have<UserRolesUpdatedEvent>();

        Assert.Equal(userRolesUpdatedEvent.Roles, roles);
    }

    [Fact]
    public void UpdateRoles_SameRoles_NotApplyChange()
    {
        var user = CreateUser();

        var roles = new Collection<string>
        {
            AuthorizationConstants.Roles.Administrator,
            AuthorizationConstants.Roles.PowerUser
        };

        user.UpdateRoles(roles);
        user.UpdateRoles(roles);
        Assert.Single(user.PopUncommittedEvents().Where(e => e.GetType() == typeof(UserRolesUpdatedEvent)));
    }

    private User CreateUser()
    {
        _mockUserChecker.Setup(u => u.IsUniqueEmail(It.IsAny<string>())).Returns(true);

        return User.Initialize(_mockUserChecker.Object, _mockPasswordService.Object, "bruce@outlook.com", "123456",
            "Bruce");
    }
}