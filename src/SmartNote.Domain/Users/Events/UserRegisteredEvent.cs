namespace SmartNote.Domain.Users.Events;

public class UserRegisteredEvent : DomainEventBase
{
    public string Email { get; }

    public UserRegisteredEvent(string email)
    {
        Email = email;
    }
}