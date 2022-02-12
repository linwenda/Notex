namespace SmartNote.Core.Domain.Users;

public class UserRegisteredEvent : DomainEventBase
{
    public string Email { get; }

    public UserRegisteredEvent(string email)
    {
        Email = email;
    }
}