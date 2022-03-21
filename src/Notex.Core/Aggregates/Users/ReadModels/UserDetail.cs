using Notex.Messages.Users.Events;

namespace Notex.Core.Aggregates.Users.ReadModels;

public class UserDetail : IReadModelEntity
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string Avatar { get; set; }
    public bool IsActive { get; set; }
    public string[] Roles { get; set; }
    public DateTimeOffset CreationTime { get; set; }

    public void When(UserCreatedEvent @event)
    {
        Id = @event.AggregateId;
        UserName = @event.UserName;
        Password = @event.Password;
        Email = @event.Email;
        Name = @event.Name;
        IsActive = true;
        CreationTime = DateTimeOffset.UtcNow;
        Roles = Array.Empty<string>();
    }

    public void When(UserPasswordChangedEvent @event)
    {
        Password = @event.Password;
    }

    public void When(UserProfileUpdatedEvent @event)
    {
        Avatar = @event.Avatar;
        Bio = @event.Bio;
        Name = @event.Name;
    }

    public void When(UserRolesUpdatedEvent @event)
    {
        Roles = @event.Roles.ToArray();
    }
}