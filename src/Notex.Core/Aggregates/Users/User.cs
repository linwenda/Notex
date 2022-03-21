using Notex.Core.Aggregates.Users.DomainServices;
using Notex.Core.Aggregates.Users.Exceptions;
using Notex.Messages;
using Notex.Messages.Users.Events;

namespace Notex.Core.Aggregates.Users;

public class User : AggregateRoot
{
    private string _userName;
    private string _password;
    private string _email;
    private string _name;
    private string _bio;
    private string _avatar;
    private bool _isActive;
    private List<string> _roles;

    private User(Guid id) : base(id)
    {
    }

    public static User Initialize(IUserChecker userChecker, IPasswordService passwordService, string email,
        string password, string name)
    {
        if (!userChecker.IsUniqueEmail(email))
        {
            throw new EmailAlreadyExistsException();
        }

        var user = new User(Guid.NewGuid());

        user.ApplyChange(new UserCreatedEvent(user.Id, user.GetNextVersion(), email,
            passwordService.HashPassword(password), email, name, true));

        return user;
    }
    
    public void ChangePassword(IPasswordService passwordService, string inputPassword, string newPassword)
    {
        if (!passwordService.VerifyHashedPassword(_password, inputPassword))
        {
            throw new PasswordIncorrectException();
        }

        ApplyChange(new UserPasswordChangedEvent(Id, GetNextVersion(), passwordService.HashPassword(newPassword)));
    }

    public void UpdateProfile(string name, string avatar, string bio)
    {
        ApplyChange(new UserProfileUpdatedEvent(Id, GetNextVersion(), name, bio, avatar));
    }

    public void UpdateRoles(ICollection<string> roles)
    {
        var inFirstOnly = _roles.Except(roles);
        var inSecondOnly = roles.Except(_roles);

        if (inFirstOnly.Any() || inSecondOnly.Any())
        {
            ApplyChange(new UserRolesUpdatedEvent(Id, GetNextVersion(), roles));
        }
    }

    protected override void Apply(IVersionedEvent @event)
    {
        this.When((dynamic) @event);
    }

    private void When(UserCreatedEvent @event)
    {
        _userName = @event.UserName;
        _password = @event.Password;
        _email = @event.Email;
        _name = @event.Name;
        _isActive = @event.IsActive;
        _roles = new List<string>();
    }

    private void When(UserPasswordChangedEvent @event)
    {
        _password = @event.Password;
    }

    private void When(UserProfileUpdatedEvent @event)
    {
        _name = @event.Name;
        _bio = @event.Bio;
        _avatar = @event.Avatar;
    }

    private void When(UserRolesUpdatedEvent @event)
    {
        _roles = @event.Roles.ToList();
    }
}